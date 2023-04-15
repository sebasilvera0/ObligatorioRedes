using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class ManejoDeConsultas
    {

        DBMemory DBMemory;


        public void ManejoDeConsultasSer(string mensaje)
        {
            // Obtener los primeros 3 caracteres del mensaje
            string codigo = mensaje.Substring(0, 3);

            // Obtener los siguientes 4 caracteres después del espacio en blanco
            string largoString = mensaje.Substring(4, 4);

            // Convertir el valor del largo a un entero
            int largo = int.Parse(largoString);

            // Obtener el mensaje después del segundo espacio en blanco
            string mensajeCompleto = mensaje.Substring(9);

            if (codigo.Equals("R00"))
            {
                this.ConsultaLogin(mensajeCompleto);
            }

        }
        

        public bool ConsultaLogin(String mensajeCompleto)
        {
            string[] partesMensaje = mensajeCompleto.Split('#');
            string usuario = partesMensaje[0];
            string contrasena = partesMensaje[1];

          return    DBMemory.logginUser(usuario, contrasena);
        }

    }
}
