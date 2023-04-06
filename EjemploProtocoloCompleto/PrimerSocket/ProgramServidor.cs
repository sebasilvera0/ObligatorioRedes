using Protocolo;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Domain;
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

                    String password = "";

                    String idUsuario = "";

                    if (LoginUsuario(idUsuario, password))
                    {
                        MuestroMenuPrincipal(manejoDataSocket, false);

                        byte[] datosLargo = manejoDataSocket.Receive(Constantes.LargoFijo);
                        byte[] datos = manejoDataSocket.Receive(BitConverter.ToInt32(datosLargo));

                        string mensaje = $"El cliente dice {Encoding.UTF8.GetString(datos)}";
                        Console.WriteLine(mensaje);
                    }
                    else
                    {

                    }
                }
                catch (SocketException e)
                {
                    clienteConectado = false;
                }

            }
            Console.WriteLine("Cliente desconectado");
        }

        static bool LoginUsuario(String idUsuario , String password)
        {
            Persistencia pers = new Domain.Persistencia();
            return pers.ValidarCredenciales(idUsuario, password);
        }


        static void MuestroMenuPrincipal(ManejoDataSocket manejoDataSocket , Boolean esAdmin)
        {
            String menus = "Menu Principal \n" +
                               "1. Dar de Alta a un usuario.\n" +
                               "2. Dar de Alta a un repuesto.\n" +
                               "3. Crear Categoría de repuesto.\n" +
                               "4. Asociar Categorías a un repuesto.\n" +
                               "5. Asociar una foto al repuesto.\n" +
                               "6. Consultar repuestos existentes.\n" +
                               "7. Consultar un repuesto específico.\n" +
                               "8. Enviar y recibir mensajes entre mecánicos.\n" +
                               "9. Configuración.\n" +
                               "10. Exit.";
            byte[] menu = Encoding.UTF8.GetBytes(menus);
            byte[] menuLargo = BitConverter.GetBytes(menu.Length);


            manejoDataSocket.Send(menuLargo); // Mando la parte fija (4 bytes)
            manejoDataSocket.Send(menu);
        }
    }
}