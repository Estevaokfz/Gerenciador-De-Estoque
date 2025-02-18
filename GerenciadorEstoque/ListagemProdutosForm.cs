using GerenciadorEstoque.Models;
using GerenciadorEstoque.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GerenciadorEstoque
{
    public class ListagemProdutosForm : Form
    {
        private DataGridView dgvProdutos;
        private Button btnFechar;

        public ListagemProdutosForm()
        {
            this.Text = "Listagem de Produtos";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;

            // Inicializa o DataGridView
            dgvProdutos = new DataGridView
            {
                Left = 10,
                Top = 10,
                Width = 560,
                Height = 300,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Adiciona as colunas no DataGridView
            dgvProdutos.Columns.Add("Nome", "Nome do Produto");
            dgvProdutos.Columns.Add("Categoria", "Categoria");
            dgvProdutos.Columns.Add("Preco", "Preço");
            dgvProdutos.Columns.Add("Quantidade", "Quantidade em Estoque");

            // Botão para fechar a listagem
            btnFechar = new Button
            {
                Text = "Fechar",
                Left = 250,
                Top = 320,
                Width = 100
            };
            btnFechar.Click += (sender, e) => this.Close();

            this.Controls.Add(dgvProdutos);
            this.Controls.Add(btnFechar);

            // Carrega os produtos na listagem
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            // Obtém a lista de produtos do repositório
            List<Produto> produtos = ProdutoRepository.ListarTodos();

            // Limpa o DataGridView
            dgvProdutos.Rows.Clear();

            // Adiciona os produtos ao DataGridView
            foreach (var produto in produtos)
            {
                dgvProdutos.Rows.Add(produto.Nome, produto.Categoria.Nome, produto.Preco, produto.Quantidade);
            }
        }
    }
}
