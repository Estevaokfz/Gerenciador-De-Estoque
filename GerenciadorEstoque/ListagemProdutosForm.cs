using System;
using System.Linq;
using System.Windows.Forms;
using GerenciadorEstoque.Models;
using GerenciadorEstoque.Repositories;

namespace GerenciadorEstoque
{
    public class ListagemProdutoForm : Form
    {
        private DataGridView dgvProdutos;
        private TextBox txtFiltroNome;
        private ComboBox cmbFiltroCategoria;
        private Button btnFiltrar;

        public ListagemProdutoForm()
        {
            this.Text = "Listagem de Produtos";
            this.Width = 800;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            // TextBox para filtro por nome
            Label lblFiltroNome = new Label() { Text = "Filtro por Nome:", Left = 20, Top = 20, Width = 100 };
            txtFiltroNome = new TextBox() { Left = 120, Top = 20, Width = 150 };

            // ComboBox para filtro por categoria
            Label lblFiltroCategoria = new Label() { Text = "Filtro por Categoria:", Left = 20, Top = 60, Width = 120 };
            cmbFiltroCategoria = new ComboBox() { Left = 140, Top = 60, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };

            // Botão para aplicar o filtro
            btnFiltrar = new Button() { Text = "Filtrar", Left = 120, Top = 100, Width = 100 };
            btnFiltrar.Click += BtnFiltrar_Click;

            // DataGridView para exibição dos produtos
            dgvProdutos = new DataGridView();
            dgvProdutos.Dock = DockStyle.Bottom;
            dgvProdutos.AutoGenerateColumns = false;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutos.AllowUserToAddRows = false;

            // Colunas
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nome", DataPropertyName = "Nome", Width = 200 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Categoria", DataPropertyName = "CategoriaNome", Width = 150 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantidade", DataPropertyName = "Quantidade", Width = 100 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Valor", DataPropertyName = "Valor", Width = 100 });

            // Botão Editar
            var btnEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true
            };
            dgvProdutos.Columns.Add(btnEditar);

            // Botão Excluir
            var btnExcluir = new DataGridViewButtonColumn
            {
                HeaderText = "Excluir",
                Text = "Excluir",
                UseColumnTextForButtonValue = true
            };
            dgvProdutos.Columns.Add(btnExcluir);

            dgvProdutos.CellClick += DgvProdutos_CellClick;

            // Carregar categorias no ComboBox de filtro
            CarregarCategorias();

            this.Controls.Add(lblFiltroNome);
            this.Controls.Add(txtFiltroNome);
            this.Controls.Add(lblFiltroCategoria);
            this.Controls.Add(cmbFiltroCategoria);
            this.Controls.Add(btnFiltrar);
            this.Controls.Add(dgvProdutos);

            CarregarProdutos();
        }

        private void CarregarCategorias()
        {
            var categorias = CategoriaRepository.ListarTodos();
            categorias.Insert(0, new Categoria { Id = 0, Nome = "Todas" }); // Adiciona opção "Todas"
            cmbFiltroCategoria.DataSource = categorias;
            cmbFiltroCategoria.DisplayMember = "Nome";
            cmbFiltroCategoria.ValueMember = "Id";
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            // Garantir que a lista de produtos não seja null
            var produtos = ProdutoRepository.ObterTodos() ?? new List<Produto>(); // Se for null, cria uma lista vazia

            // Obtendo os valores dos filtros
            string filtroNome = txtFiltroNome.Text.ToLower();
            int filtroCategoriaId = (int)(cmbFiltroCategoria.SelectedValue ?? 0); // Se o valor for null, define 0

            // Filtrando produtos
            var produtosFiltrados = produtos
                .Where(p => (string.IsNullOrEmpty(filtroNome) || p.Nome.ToLower().Contains(filtroNome)) &&
                           (filtroCategoriaId == 0 || p.Categoria?.Id == filtroCategoriaId)) // Verifica se Categoria não é null
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    CategoriaNome = p.Categoria?.Nome ?? "Sem Categoria", // Caso a categoria seja null, exibe "Sem Categoria"
                    p.Quantidade,
                    p.Valor
                })
                .ToList();

            // Atualiza a DataGridView com os produtos filtrados
            dgvProdutos.DataSource = produtosFiltrados;
        }

        private void DgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var produtoId = (int)dgvProdutos.Rows[e.RowIndex].Cells[0].Value;

            if (e.ColumnIndex == 5) // Editar
            {
                EditarProduto(produtoId);
            }
            else if (e.ColumnIndex == 6) // Excluir
            {
                ExcluirProduto(produtoId);
            }
        }

        private void EditarProduto(int id)
        {
            var produto = ProdutoRepository.ObterPorId(id);
            if (produto != null)
            {
                using (var form = new CadastroProdutoForm(produto))
                {
                    form.ShowDialog();
                }
                CarregarProdutos();
            }
        }

        private void ExcluirProduto(int id)
        {
            var confirm = MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                ProdutoRepository.Remover(id);
                CarregarProdutos();
            }
        }
    }
}
