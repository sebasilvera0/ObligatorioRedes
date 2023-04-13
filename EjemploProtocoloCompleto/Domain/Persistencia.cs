using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Persistencia
    {
        private static List<Usuario> _usuarios = new List<Usuario>();

        public Persistencia()
        {
            // Agregar usuarios a la lista
            _usuarios.Add(new Usuario("user1", "123"));
            _usuarios.Add(new Usuario("user2", "abc"));
        }

        public  bool ValidarCredenciales(string usuario, string password)
        {
             foreach (var u in _usuarios)
            {
                if (u.Name == usuario)
                {
                     if (u.Password == password)
                    {
                         return true;
                    }
                    else
                    {
                          return false;
                    }
                }
            }
             return false;
        }
    }
}