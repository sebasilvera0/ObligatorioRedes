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
            Usuario us = new Usuario("Juan", "Juan");
            listaUsuarios.Add(us);
        }

        public bool logginUser(String user, String contrasena)
        {
            Usuario usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.NombreUsuario == user && u.Contrasena == contrasena);

            return usuarioEncontrado != null;

        }

    }
}
