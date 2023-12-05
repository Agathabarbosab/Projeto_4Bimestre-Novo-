using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_4Bimestre
{
    internal class UserDAO
    {
        public List<User> SelectUser()
        {
            Connection conn = new Connection();
            SqlCommand sqlCom = new SqlCommand();

            sqlCom.Connection = conn.ReturnConnection();
            sqlCom.CommandText = "SELECT * FROM Table_1";

            List<User> user = new List<User>();
            try
            {
                SqlDataReader dr = sqlCom.ExecuteReader();

                //Enquanto for possível continuar a leitura das linhas que foram retornadas na consulta, execute..
                while (dr.Read())
                {
                    User objeto = new User(
                    (int)dr["Id"],
                    (string)dr["Name"],
                    (string)dr["Password"]
                    );

                    user.Add(objeto);
                }
                dr.Close();
                return user;//retornar a lista
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
            return null;
        }
        private bool Checkpassword(string password, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(password);
            return string.Equals(hashedPassword, hashedInputPassword);
        }
        // Método para gerar o hash SHA-256 de uma senha
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool LoginUser(string user, string password)
        {
            Connection conn = new Connection();
            SqlCommand sqlCom = new SqlCommand();
            sqlCom.Connection = conn.ReturnConnection();
            sqlCom.CommandText = "SELECT * FROM Table_1 WHERE" +
                " name = @name";
            sqlCom.Parameters.AddWithValue("@name", user);
            try
            {
                SqlDataReader dr = sqlCom.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    string hashedPassword = (string)dr["Password"];
                    dr.Close();
                    if (Checkpassword(password, hashedPassword))
                    {
                        return true;
                    }
                    return true;
                }
                dr.Close();
                return false;
            }
            catch (Exception err)
            {
                throw new Exception(
                    "Erro na leitura de Dados \n" +
                    err.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }


        //CRUD
        //UPDATE
        public void UpdateUser(User user)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"UPDATE table_1 SET
            name = @name,
            Password = @Password
            WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            sqlCommand.Parameters.AddWithValue("@Password", user.Password);
            sqlCommand.Parameters.AddWithValue("@id", user.Id);
            sqlCommand.ExecuteNonQuery();
        }
        public void InsertUser(User user)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"INSERT INTO Table_1 (name, password) VALUES 
           (@name, @password)";

            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            //Hash da senha
            string hashedPassword = HashPassword(user.Password);
            sqlCommand.Parameters.AddWithValue("@password", hashedPassword);
            sqlCommand.ExecuteNonQuery();
        }

        //DELETAR
        //CRUD
        public void DeleteUser(int id)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"DELETE FROM Table_1 WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw new Exception("Erro: Problemas ao excluir usuário no banco.\n" + err.Message);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
