using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class ManejoDeConsultas
    {

        


        public String ManejoDeConsultasSer(string mensaje)
        {
            String respuesta = "";
            // Obtener los primeros 3 caracteres del mensaje
            string codigo = mensaje.Substring(0, 3);

            // Obtener los siguientes 4 caracteres después del espacio en blanco
            //string largoString = mensaje.Substring(4, 4);

            // Convertir el valor del largo a un entero
           // int largo = int.Parse(largoString);

            // Obtener el mensaje después del segundo espacio en blanco
            string mensajeCompleto = mensaje.Substring(3);

            if (codigo.Equals("R00"))
            {
               return  this.ConsultaLogin(mensajeCompleto);
            }


            return respuesta;

        }

        
        

        public String ConsultaLogin(String mensajeCompleto)
        {
            String respuesta = "";
            string[] partesMensaje = mensajeCompleto.Split('#');
            string usuario = partesMensaje[0];
            string contrasena = partesMensaje[1];

            DBMemory mem = new DBMemory();
            Usuario usuarioLogueado = mem.logginUser(usuario, contrasena);
            if(usuarioLogueado == null)
            {
                return "404";
            }
            return (usuarioLogueado.EsAdmin) ? ("200Admin") : ("200");
        }

    }
}
