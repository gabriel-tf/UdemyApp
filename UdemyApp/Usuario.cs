using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyApp
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        internal static Type getType()
        {
            throw new NotImplementedException();
        }
    }
}
