
using GerenciadorEstoque.Models; // Importa o namespace da Categoria
using GerenciadorEstoque.Repositories; // Certifique-se de que o repositório está sendo utilizado corretamente
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
        private NumericUpDown numQuantidade;
        private TextBox txtPreco;
        private Button btnSalvar;



        public CadastroProdutoForm()
        {
            this.Text = "Cadastro de Produto";
            this.Width = 400;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNome = new Label() { Text = "Nome:", Left = 20, Top = 20, Width = 100 };
            txtNome = new TextBox() { Left = 120, Top = 20, Width = 200 };

            Label lblCategoria = new Label() { Text = "Categoria:", Left = 20, Top = 60, Width = 100 };
            cmbCategoria = new ComboBox() { Left = 120, Top = 60, Width = 200 };

            Label lblQuantidade = new Label() { Text = "Quantidade:", Left = 20, Top = 100, Width = 100 };
            numQuantidade = new NumericUpDown() { Left = 120, Top = 100, Width = 100 };

            Label lblPreco = new Label() { Text = "Preço:", Left = 20, Top = 140, Width = 100 };
            txtPreco = new TextBox() { Left = 120, Top = 140, Width = 100 };

            btnSalvar = new Button() { Text = "Salvar", Left = 120, Top = 180, Width = 100 };
            btnSalvar.Click += BtnSalvar_Click;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(lblCategoria);
            this.Controls.Add(cmbCategoria);
            this.Controls.Add(lblQuantidade);
            this.Controls.Add(numQuantidade);
            this.Controls.Add(lblPreco);
            this.Controls.Add(txtPreco);
            this.Controls.Add(btnSalvar);



            CarregarCategorias();
        }

        public void CarregarCategorias()
        {
            // Obtém todas as categorias do repositório
            var categorias = CategoriaRepository.ListarTodos();

            // Define a origem de dados da ComboBox com as categorias
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nome"; // O que vai aparecer na interface
            cmbCategoria.ValueMember = "Id";     // O valor interno que fica "escondido"
        }




        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // Verifica se há uma categoria selecionada
            if (cmbCategoria.SelectedItem is Categoria categoriaSelecionada)
            {
                // Cria um novo produto com as informações preenchidas
                Produto novoProduto = new Produto
                {
                    Nome = txtNome.Text,
                    Categoria = categoriaSelecionada,  // Aqui agora vai o objeto completo
                    Preco = decimal.Parse(txtPreco.Text),
                    Quantidade = int.Parse(numQuantidade.Text)
                };

                // Adiciona o produto ao repositório
                ProdutoRepository.Adicionar(novoProduto);

                MessageBox.Show("Produto cadastrado com sucesso!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione uma categoria válida.");
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório.");
                return false;
            }

            if (cmbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione uma categoria.");
                return false;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco < 0)
            {
                MessageBox.Show("Informe um preço válido.");
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            cmbCategoria.SelectedIndex = -1;
            numQuantidade.Value = 0;
            txtPreco.Clear();
        }
        private void AbrirCadastroProduto()
        {
            using (var form = new CadastroProdutoForm())
            {
                form.ShowDialog();
            }
            // Recarregar categorias após o fechamento
            CarregarCategorias();
        }

    }
}
