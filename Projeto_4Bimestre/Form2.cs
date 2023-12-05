using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_4Bimestre
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = maskedTextBox1.Text;
            String password = textBox2.Text;
            try
            {
                UserDAO user = new UserDAO();
                // Criptografar a senha antes de verificar o login
                string hashedPassword = User.CriptografarPassword(password);
                if (user.LoginUser(name, hashedPassword))
                {
                    Form4 Form4 = new Form4();
                    Form4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Errado!",
                        "Nome e Senha",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private bool ValidarNomeUsuario(string nomeUsuario)
        {
            // Lógica de validação do nome do usuário (substitua com sua lógica real)
            // Por exemplo, verificar se o nome do usuário atende a certos critérios.

            // Vamos usar um exemplo simples aqui: o nome do usuário não pode estar vazio.
            return !string.IsNullOrEmpty(nomeUsuario);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserDAO userDAO = new UserDAO();
            string nomeUsuario = maskedTextBox1.Text;
            string senha = textBox2.Text;

            try
            {
                if (userDAO.LoginUser(nomeUsuario, senha))
                {
                    // Usuário válido
                    checkBox1.Checked = true;
                    MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Usuário inválido
                    checkBox1.Checked = false;
                    MessageBox.Show("Nome de usuário ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar o login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }
    }
}
