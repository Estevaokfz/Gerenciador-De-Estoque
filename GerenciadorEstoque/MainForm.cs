using System;
using System.Windows.Forms;

namespace GerenciadorEstoque
{
    public class MainForm : Form
    {
        private MenuStrip menuStrip;

        public MainForm()
        {
            // Configurações do Form
            this.Text = "Gerenciador de Estoque";
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Menu Strip
            menuStrip = new MenuStrip();
            var menuCadastro = new ToolStripMenuItem("Cadastro");
            var menuEstoque = new ToolStripMenuItem("Estoque");

            var menuCadastroProduto = new ToolStripMenuItem("Produtos");
            var menuCadastroCategoria = new ToolStripMenuItem("Categorias");

            menuCadastro.DropDownItems.Add(menuCadastroProduto);
            menuCadastro.DropDownItems.Add(menuCadastroCategoria);

            menuStrip.Items.Add(menuCadastro);
            menuStrip.Items.Add(menuEstoque);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Eventos de Clique
            menuCadastroProduto.Click += (s, e) =>
            {
                var form = new CadastroProdutoForm();
                form.ShowDialog();
            };

            menuCadastroCategoria.Click += (s, e) =>
            {
                var form = new CadastroCategoriaForm();
                form.ShowDialog();
            };
        }
    }
}