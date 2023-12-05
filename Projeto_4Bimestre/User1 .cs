using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_4Bimestre
{
    internal class User1
    {
        private int _id;
        private string _email;
        private string _numero;

        public User1(string email,
            string numero)
        {
            Email = email;
            Numero = numero;
        }
        public User1(int id,
            string email,
            string numero)
        {
            Email = email;
            Numero = numero;
            Id = id;
        }
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Email
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Campo Nome está vazio");
                _email = value;
            }
            get { return _email; }
        }
        public string Numero
        {
            set { _numero = value; }
            get { return _numero; }
        }
    }
}
