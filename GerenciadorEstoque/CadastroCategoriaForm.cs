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

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Categoria salva com sucesso!");
            this.Close();
        }
    }
}
