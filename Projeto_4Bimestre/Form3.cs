using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Projeto_4Bimestre
{
    public partial class Form3 : Form
    {
        private readonly string DataBase = "Projeto_4Bimestre";
        private int id;
        public Form3()
        {
            InitializeComponent();
        }
        private void UpdateListView()
        {
            listView1.Items.Clear();
            UserDAO userDAO = new UserDAO();
            List<User> users = userDAO.SelectUser();
            try
            {
                foreach (User user in users)
                {
                    //Enquanto for possível continuar a leitura das linhas que foram retornadas na consulta, execute.
                    ListViewItem lv = new ListViewItem(user.Id.ToString());
                    lv.SubItems.Add(user.Name);
                    lv.SubItems.Add(user.Password);
                    listView1.Items.Add(lv);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //botão Logar
        private void button1_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            String password = textBox2.Text;
            try
            {
                UserDAO user = new UserDAO();

                // Criptografar a senha antes de verificar o login
                string hashedPassword = User.CriptografarPassword(password);

                if (user.LoginUser(name, hashedPassword))
                {
                    Form1 Form1 = new Form1();
                    Form1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nome de usuário ou senha incorretos!",
                        "Erro de Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        //botão cadastrar
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User(
                    textBox1.Text,
                    textBox2.Text
                );
                UserDAO nomeDoObj = new UserDAO();
                nomeDoObj.InsertUser(user);

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

        //CRUD
        //READ
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
        }
        //botão deletar
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User(
                            textBox1.Text,
                            textBox2.Text);
                //chamando o metodo de exclusão
                UserDAO nomeDoObj = new UserDAO();
                nomeDoObj.DeleteUser(id);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            textBox1.Clear();
            textBox2.Clear();
            UpdateListView();
        }
        //botão editar
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User(
                        id,
                        textBox1.Text,
                        textBox2.Text);
                //chamando o metodo de exclusão
                UserDAO nomeDoObj = new UserDAO();
                nomeDoObj.UpdateUser(user);
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
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Arquivos PDF (*.pdf)|*.pdf|Todos os arquivos (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Conexão com o banco de dados SQL Server
                string stringConnection = @"Data Source="
                         + Environment.MachineName +
                         @"\SQLEXPRESS;Initial Catalog=" +
                         DataBase + ";Integrated Security=true";
                SqlConnection connection = new SqlConnection(stringConnection);
                connection.Open();

                string query = "SELECT name, password FROM Table_1";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Document document = new Document();

                try
                {
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                    document.Open();

                    PdfPTable table = new PdfPTable(2);
                    table.AddCell("name");
                    table.AddCell("password");

                    while (reader.Read())
                    {
                        table.AddCell(reader["name"].ToString());
                        table.AddCell(reader["password"].ToString());
                    }

                    document.Add(table);

                    MessageBox.Show("Relatório gerado com sucesso", "Êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    document.Close();
                    connection.Close();
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }
    }
}
