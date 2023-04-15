using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{
    public  class Usuario
    {
        public String NombreUsuario { get; set; }
        public String Contrasena { get; set; }
      


        public Usuario(String name, String password)
        {
            NombreUsuario = name;
            Contrasena = password;
         
        }
    }
}
