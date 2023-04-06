using Protocolo;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PrimerSocket
{
    internal class ProgramServidor
    {
        static int cantClientes = 3;
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Servidor");
            // 1- Se crea el Socket del servidor
            var socketServidor = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream, 
                ProtocolType.Tcp);

            // 2- Creamos un EndPoint con Ip = localHost + Puerto
            var endPointServidor = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);

            // 3- Asociamos el socket con el EndPoint por medio del Bind
            socketServidor.Bind(endPointServidor);

            // 4- Ponemos el socket en modo escucha
            socketServidor.Listen(10);

            int clientesConectados = 0;

            while (clientesConectados < cantClientes)
            {
                // 5- Aceptamos N conexiones
                var socketCliente = socketServidor.Accept(); //Bloquea el hilo principal del Servidor, queda esperando por una conexión de un Cliente
                Console.WriteLine("Cliente conectado!");
                clientesConectados++;
                // Disparamos un hilo para manejar cada Cliente
                new Thread(() => ManejoCliente(socketCliente)).Start();
            }
            Console.ReadLine();
        }

        static void ManejoCliente(Socket socketCliente)
        {
            bool clienteConectado = true;
            ManejoDataSocket manejoDataSocket = new ManejoDataSocket(socketCliente);
            while (clienteConectado)
            {
                try
                {
                    byte[] datosLargo = manejoDataSocket.Receive(Constantes.LargoFijo);
                    byte[] datos = manejoDataSocket.Receive(BitConverter.ToInt32(datosLargo));

                    string mensaje = $"El cliente dice {Encoding.UTF8.GetString(datos)}";
                    Console.WriteLine(mensaje);

                    String menus = "Esto es el menu";
                    byte[] menu = Encoding.UTF8.GetBytes(menus);
                    byte[] menuLargo = BitConverter.GetBytes(menu.Length);


                    manejoDataSocket.Send(menuLargo); // Mando la parte fija (4 bytes)
                    manejoDataSocket.Send(menu);


                }
                catch (SocketException e)
                {
                    clienteConectado = false;
                }

            }
            Console.WriteLine("Cliente desconectado");
        }
    }
}