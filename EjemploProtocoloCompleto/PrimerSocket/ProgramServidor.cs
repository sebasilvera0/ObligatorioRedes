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
            var socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endPointServidor = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
            socketServidor.Bind(endPointServidor);
            socketServidor.Listen(10);

            int clientesConectados = 0;
            while (clientesConectados < cantClientes)
            {
                var socketCliente = socketServidor.Accept();
                Console.WriteLine("Cliente conectado!");
                clientesConectados++;
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

                    if (!LoginUsuario(idUsuario, password))
                    {
                        MuestroMenuPrincipal(manejoDataSocket, false);
                        ManejoMenu(manejoDataSocket);
                    }
                    // else
                    // {
                    //
                    // }
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
            
            manejoDataSocket.Send(menuLargo);
            manejoDataSocket.Send(menu);
        }

        static void ManejoMenu(ManejoDataSocket manejoDataSocket)
        {
            byte[] headLargo = manejoDataSocket.Receive(Constantes.HeadFijo);
            byte[] cmdLargo = manejoDataSocket.Receive(Constantes.CmdFijo);
            //byte[] datosLargo = manejoDataSocket.Receive(Constantes.LargoFijo);
            
            byte[] head = manejoDataSocket.Receive(BitConverter.ToInt32(headLargo));
            byte[] cmd = manejoDataSocket.Receive(BitConverter.ToInt32(cmdLargo));
            //byte[] datos = manejoDataSocket.Receive(BitConverter.ToInt32(datosLargo));

            string headCmd = $"{Encoding.UTF8.GetString(head)} + {Encoding.UTF8.GetString(cmd)}";
            // string mensaje = $"{Encoding.UTF8.GetString(datos)}";
            // string[] mensajeDecodificado = mensaje.Split("#");

            switch (headCmd)
            {
                case "REQ01":
                    //1. Dar de Alta a un usuario
                    break;
                case "REQ02":
                    //2. Dar de Alta a un repuesto.
                    break;
                case "REQ03":
                    //3. Crear Categoría de repuesto.
                    break;
                case "REQ04":
                    //4. Asociar Categorías a un repuesto.
                    break;
                case "REQ05":
                    //5. Asociar una foto al repuesto.
                    break;
                case "REQ06":
                    //6. Consultar repuestos existentes.
                    break;
                case "REQ07":
                    //7. Consultar un repuesto específico.
                    break;
                case "REQ08":
                    //8. Enviar y recibir mensajes entre mecánicos.
                    break;
                case "REQ09":
                    //9. Configuración.
                    break;
                case "REQ10":
                    //10. Exit.
                    break;
            }
        }
    }
}