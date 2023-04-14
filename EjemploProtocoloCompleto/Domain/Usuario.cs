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
        public String Name { get; set; }
        public String Password { get; set; }
        
        public Usuario(String name, String password)
        {
            Name = name;
            Password = password;
         
        }
    }
}
