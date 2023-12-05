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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            String numero = textBox2.Text;
            try
            {
                UserDAO1 user1 = new UserDAO1();
                if (user1.LoginUser1(email, numero))
                {
                    Form8 Form8 = new Form8();
                    Form8.Show();
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

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.Show();
            this.Hide();
        }
    }
}
