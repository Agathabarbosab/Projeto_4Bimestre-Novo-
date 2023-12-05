using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_4Bimestre
{
    internal class User
    {
        private int _id;
        private string _name;
        private string _password;

        public User(string name,
            string password)
        {
            Name = name;
            Password = CriptografarPassword(password);
        }
        public User(int id,
            string name,
            string password)
        {
            Name = name;
            Password = CriptografarPassword(password);
            Id = id;
        }
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Name
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Campo Nome está vazio");
                _name = value;
            }
            get { return _name; }
        }
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        public static string CriptografarPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converte a senha em bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Calcula o hash SHA-256
                byte[] hash = sha256.ComputeHash(bytes);

                // Converte o hash de volta para uma string hexadecimal
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
