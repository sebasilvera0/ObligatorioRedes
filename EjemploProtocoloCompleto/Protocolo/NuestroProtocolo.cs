using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    //Procolo C--> S   RXX (codigo interno) xxxx largo del mensaje  -- mensajePropiamenteDicho  
    public class NuestroProtocolo
    {
        String mensajeEnProtocolo;
        String Codigo;
        int LargoEnBytes;


     public  NuestroProtocolo(String codigo , String mensaje)
        {
            mensajeEnProtocolo = codigo + mensaje.Length + mensaje;
        }
    }

    
}
