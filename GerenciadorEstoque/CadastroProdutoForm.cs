using System;
using System.Windows.Forms;

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
            cmbCategoria.Items.AddRange(new string[] { "Categoria 1", "Categoria 2" }); // Mock

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
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Produto salvo com sucesso!");
            this.Close();
        }
    }
}