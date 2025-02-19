using System;
using System.Windows.Forms;

namespace GerenciadorEstoque
{
    public class MainForm : Form
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuCadastro;
        private ToolStripMenuItem menuEstoque;
        private ToolStripMenuItem menuCadastroProduto;
        private ToolStripMenuItem menuCadastroCategoria;
        private ToolStripMenuItem menuListagemProduto;

        public MainForm()
        {
            // Configurações do Form
            this.Text = "Gerenciador de Estoque";
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicializando o Menu Strip
            menuStrip = new MenuStrip();
            menuCadastro = new ToolStripMenuItem("Cadastro");
            menuEstoque = new ToolStripMenuItem("Estoque");

            menuCadastroProduto = new ToolStripMenuItem("Produtos");
            menuCadastroCategoria = new ToolStripMenuItem("Categorias");
            menuListagemProduto = new ToolStripMenuItem("Listar Produtos");

            // Adicionando itens no menu
            menuCadastro.DropDownItems.Add(menuCadastroProduto);
            menuCadastro.DropDownItems.Add(menuCadastroCategoria);
            menuEstoque.DropDownItems.Add(menuListagemProduto);

            menuStrip.Items.Add(menuCadastro);
            menuStrip.Items.Add(menuEstoque);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Eventos de Clique
            menuCadastroProduto.Click += (s, e) => AbrirCadastroProduto();
            menuCadastroCategoria.Click += (s, e) => AbrirCadastroCategoria();
            menuListagemProduto.Click += (s, e) => AbrirListagemProduto();
        }

        private void AbrirCadastroProduto()
        {
            using (var form = new CadastroProdutoForm())
            {
                form.ShowDialog();
            }
        }

        private void AbrirCadastroCategoria()
        {
            using (var form = new CadastroCategoriaForm())
            {
                form.ShowDialog();
            }
        }

        private void AbrirListagemProduto()
        {
            using (var form = new ListagemProdutoForm())
            {
                form.ShowDialog();
            }
        }
        
    }
}
