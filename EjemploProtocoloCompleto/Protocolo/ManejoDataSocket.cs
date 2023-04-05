using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Protocolo
{
    public class ManejoDataSocket
    {
        private readonly Socket _socket;

        public ManejoDataSocket(Socket socket)
        {
            _socket = socket;
        }

        public void Send(byte[] buffer) //Los datos ya vienen convertidos a byte[]
        {
            int offset = 0;
            int size = buffer.Length;

            while (offset < size) //Sigo enviando datos hasta que llegue al final
            {
                int sent = _socket.Send(buffer, offset, size - offset, SocketFlags.None);
                if (sent == 0) //Deja de enviar datos antes qye se envien la cantidad esperada
                { throw new SocketException(); }

                offset += sent; //La siguiente iteración la empiezo donde terminó la anterior
            }
        }

        public byte[] Receive(int size)
        {
            byte[] buffer = new byte[size];
            int offset = 0;

            while (offset < size) //Sigo recibiendo datos hasta completar el array respuesta
            {
                int received = _socket.Receive(buffer, offset, size - offset, SocketFlags.None);
                if (received == 0) //Deja de recibir datos antes de que llegue a la cantidad esperada
                { throw new SocketException(); }

                offset += received; //La siguiente iteración la empiezo donde terminó la anterior
            }

            return buffer;
        }
    }
}
