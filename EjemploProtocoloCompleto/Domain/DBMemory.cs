using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class DBMemory
    {
        List<Usuario> listaUsuarios = new List<Usuario>();

        public DBMemory()
        {
            Usuario us = new Usuario("Juan", "Juan",false);
            listaUsuarios.Add(new Usuario("user1", "123",false));
            listaUsuarios.Add(new Usuario("user2", "abc", false));
            listaUsuarios.Add(new Usuario("admin", "admin", true));
            listaUsuarios.Add(us);
        }

        public Usuario logginUser(String user, String contrasena)
        {
            Usuario usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.NombreUsuario == user && u.Contrasena == contrasena);

            return usuarioEncontrado;

        }

    }
}
