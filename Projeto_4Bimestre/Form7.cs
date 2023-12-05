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
    public partial class Form7 : Form
    {
        private int id;
        public Form7()
        {
            InitializeComponent();
            UpdateListView();
        }
        private void UpdateListView()
        {
            listView1.Items.Clear();
            UserDAO1 userDAO1 = new UserDAO1();
            List<User1> users1 = userDAO1.SelectUser1();
            try
            {
                foreach (User1 user1 in users1)
                {
                    //Enquanto for possível continuar a leitura das linhas que foram retornadas na consulta, execute.
                    ListViewItem lv = new ListViewItem(user1.Id.ToString());
                    lv.SubItems.Add(user1.Email);
                    lv.SubItems.Add(user1.Numero);
                    listView1.Items.Add(lv);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                User1 user1 = new User1(
                    textBox1.Text,
                    textBox2.Text
                );
                UserDAO1 nomeDoObj = new UserDAO1();
                nomeDoObj.InsertUser1(user1);

                MessageBox.Show("Cadastro feito com sucesso !",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                UpdateListView();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                User1 user1 = new User1(
                            textBox1.Text,
                            textBox2.Text);
                //chamando o metodo de exclusão
                UserDAO1 nomeDoObj = new UserDAO1();
                nomeDoObj.DeleteUser1(id);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            textBox1.Clear();
            textBox2.Clear();
            UpdateListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                User1 user1 = new User1(
                        id,
                        textBox1.Text,
                        textBox2.Text);
                //chamando o metodo de exclusão
                UserDAO1 nomeDoObj = new UserDAO1();
                nomeDoObj.UpdateUser1(user1);
                MessageBox.Show(
                 "Login alterado com sucesso !",
                 "AVISO",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information
                 );
                textBox1.Clear();
                textBox2.Clear();
                UpdateListView();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Declaração de uma variável local chamada "index" do tipo int
            int index;
            // Obtém o índice do item que está atualmente com foco no ListView
            index = listView1.FocusedItem.Index;
            // Obtém o valor do subitem na coluna 0 (primeira coluna) do item clicado e converte para int, atribuindo a variável "id"
            id = int.Parse(listView1.Items[index].SubItems[0].Text);
            // Obtém o valor do subitem na coluna 1 (segunda coluna) do item clicado e atribui ao texto da caixa de texto "textBox1"
            textBox1.Text = listView1.Items[index].SubItems[1].Text;
            textBox2.Text = listView1.Items[index].SubItems[2].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.Show();
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }
    }
}
