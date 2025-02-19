
using System;
using System.Linq;
using System.Windows.Forms;
using GerenciadorEstoque.Models;
using GerenciadorEstoque.Repositories;

namespace GerenciadorEstoque
{
    public class CadastroProdutoForm : Form
    {
        private TextBox txtNome;
        private ComboBox cmbCategoria;
        private TextBox txtQuantidade;
        private TextBox txtValor;
        private Button btnSalvar;
        private Produto produtoAtual; // Armazena o produto a ser editado

        public CadastroProdutoForm(Produto produto = null)
        {
            this.Text = produto == null ? "Cadastro de Produto" : "Editar Produto";
            this.Width = 300;
            this.Height = 250;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNome = new Label() { Text = "Nome:", Left = 20, Top = 20, Width = 100 };
            txtNome = new TextBox() { Left = 120, Top = 20, Width = 150 };

            Label lblCategoria = new Label() { Text = "Categoria:", Left = 20, Top = 60, Width = 100 };
            cmbCategoria = new ComboBox() { Left = 120, Top = 60, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblQuantidade = new Label() { Text = "Quantidade:", Left = 20, Top = 100, Width = 100 };
            txtQuantidade = new TextBox() { Left = 120, Top = 100, Width = 150 };

            Label lblValor = new Label() { Text = "Valor:", Left = 20, Top = 140, Width = 100 };
            txtValor = new TextBox() { Left = 120, Top = 140, Width = 150 };

            btnSalvar = new Button() { Text = "Salvar", Left = 100, Top = 180, Width = 100 };
            btnSalvar.Click += BtnSalvar_Click;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblCategoria);
            this.Controls.Add(cmbCategoria);
            this.Controls.Add(lblQuantidade);
            this.Controls.Add(txtQuantidade);
            this.Controls.Add(lblValor);
            this.Controls.Add(txtValor);
            this.Controls.Add(btnSalvar);

            CarregarCategorias();

            if (produto != null) // Se um produto foi passado, preencher os campos para edição
            {
                produtoAtual = produto;
                txtNome.Text = produto.Nome;
                cmbCategoria.SelectedItem = produto.Categoria?.Nome;
                txtQuantidade.Text = produto.Quantidade.ToString();
                txtValor.Text = produto.Valor.ToString("F2"); // Formato com 2 casas decimais
            }
        }

        private void CarregarCategorias()
        {
            var categorias = CategoriaRepository.ListarTodos();
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nome";
            cmbCategoria.ValueMember = "Id";
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade))
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            if (!decimal.TryParse(txtValor.Text, out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Valor inválido ou negativo.");
                return;
            }

            var categoriaSelecionada = cmbCategoria.SelectedItem as Categoria;

            if (produtoAtual == null)
            {
                // Novo produto
                var novoProduto = new Produto
                {
                    Nome = txtNome.Text,
                    Categoria = categoriaSelecionada,
                    Quantidade = quantidade,
                    Valor = valor
                };
                ProdutoRepository.Adicionar(novoProduto);
            }
            else
            {
                // Atualizando um produto existente
                produtoAtual.Nome = txtNome.Text;
                produtoAtual.Categoria = categoriaSelecionada;
                produtoAtual.Quantidade = quantidade;
                produtoAtual.Valor = valor;
                ProdutoRepository.Atualizar(produtoAtual);
            }

            MessageBox.Show("Produto salvo com sucesso!");
            this.Close();
        }
    }
}
