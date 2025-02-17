using GerenciadorEstoque.Models; // Importa o namespace da Categoria
using GerenciadorEstoque.Repositories; // Certifique-se de que o repositório está sendo utilizado corretamente
using System;
using System.Windows.Forms;

namespace GerenciadorEstoque
{
    public class CadastroCategoriaForm : Form
    {
        private TextBox txtNome;
        private Button btnSalvar;

        public CadastroCategoriaForm()
        {
            this.Text = "Cadastro de Categoria";
            this.Width = 300;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNome = new Label() { Text = "Nome:", Left = 20, Top = 20, Width = 100 };
            txtNome = new TextBox() { Left = 120, Top = 20, Width = 100 };

            btnSalvar = new Button() { Text = "Salvar", Left = 100, Top = 60, Width = 100 };
            btnSalvar.Click += BtnSalvar_Click;

            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            this.Controls.Add(btnSalvar);
        }

        // Método de evento para o clique no botão "Salvar"
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome da categoria é obrigatório.");
                return;
            }

            // Cria uma nova categoria com os dados preenchidos no formulário
            Categoria novaCategoria = new Categoria
            {
                Nome = txtNome.Text  // Usa txtNome, já que este é o nome correto do TextBox
            };

            // Chama o método do repositório para adicionar a categoria
            CategoriaRepository.Adicionar(novaCategoria);

            // Exibe mensagem de sucesso
            MessageBox.Show("Categoria salva com sucesso!");

            // Fecha o formulário após salvar
            this.Close();
        }
    }
}
