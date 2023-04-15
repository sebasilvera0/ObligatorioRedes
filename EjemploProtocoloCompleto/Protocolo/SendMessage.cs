using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    public class SendMessage
    {
        //Procolo C--> S   RXX (codigo interno) xxxx largo del mensaje  -- mensajePropiamenteDicho

        public void SendMessageToServer(String codigo,String mensaje, Socket socketCliente) {
            ManejoDataSocket manejoDataSocket = new ManejoDataSocket(socketCliente);
            this.ConvertirANuestroProcolo(codigo, mensaje);           
            try
            {
                byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                byte[] datosLargo = BitConverter.GetBytes(datos.Length);
                manejoDataSocket.Send(datosLargo); // Mando la parte fija (4 bytes)
                manejoDataSocket.Send(datos);
            }
            catch (SocketException)
            {
                Console.WriteLine("La conexión con el servidor se ha cortado");                
            }

        }

        public String ConvertirANuestroProcolo(String codigo, String mensaje)
        {
            String mensajeEnProtocolo = "";
            if (codigo != null && mensaje != null)
            {           
                mensajeEnProtocolo = codigo + mensaje.Length + mensaje;              
            }
            return mensajeEnProtocolo;
        }
    }
}
