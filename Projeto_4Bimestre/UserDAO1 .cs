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
    internal class UserDAO1
    {
        public List<User1> SelectUser1()
        {
            Connection conn = new Connection();
            SqlCommand sqlCom = new SqlCommand();

            sqlCom.Connection = conn.ReturnConnection();
            sqlCom.CommandText = "SELECT * FROM Table_2";

            List<User1> user1 = new List<User1>();
            try
            {
                SqlDataReader dr = sqlCom.ExecuteReader();

                //Enquanto for possível continuar a leitura das linhas que foram retornadas na consulta, execute..
                while (dr.Read())
                {
                    User1 objeto = new User1(
                    (int)dr["Id"],
                    (string)dr["Email"],
                    (string)dr["Numero"]
                    );

                    user1.Add(objeto);
                }
                dr.Close();
                return user1;//retornar a lista
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

        public bool LoginUser1(string email, string numero)
        {
            Connection conn = new Connection();
            SqlCommand sqlCom = new SqlCommand();
            sqlCom.Connection = conn.ReturnConnection();
            sqlCom.CommandText = "SELECT * FROM Table_2 WHERE" +
                " email = @email AND numero = @numero";
            sqlCom.Parameters.AddWithValue("@email", email);
            sqlCom.Parameters.AddWithValue("@numero", numero);
            try
            {
                SqlDataReader dr = sqlCom.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
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
        public void UpdateUser1(User1 user1)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"UPDATE table_2 SET
            email = @email,
            numero = @numero
            WHERE id = @id";
            sqlCommand.Parameters.AddWithValue("@email", user1.Email);
            sqlCommand.Parameters.AddWithValue("@numero", user1.Numero);
            sqlCommand.Parameters.AddWithValue("@id", user1.Id);
            sqlCommand.ExecuteNonQuery();
        }
        public void InsertUser1(User1 user1)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"INSERT INTO Table_2 (email, numero) VALUES 
           (@email, @numero)";

            sqlCommand.Parameters.AddWithValue("@email", user1.Email);
            sqlCommand.Parameters.AddWithValue("@numero", user1.Numero);
            sqlCommand.ExecuteNonQuery();
        }

        //DELETAR
        //CRUD
        public void DeleteUser1(int id)
        {
            Connection connection = new Connection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = connection.ReturnConnection();
            sqlCommand.CommandText = @"DELETE FROM Table_2 WHERE id = @id";
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
