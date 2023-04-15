using Protocolo;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Domain;
using Servidor;
using System.Runtime.CompilerServices;

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

                    string mensaje = Encoding.UTF8.GetString(datos);

                    ManejoDeConsultas mj = new ManejoDeConsultas();
                    String loginUser  = mj.ManejoDeConsultasSer(mensaje);
                    MuestroMenuPrincipal(manejoDataSocket, loginUser);

                    if (1==1)
                    {  
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


        static void MuestroMenuPrincipal(ManejoDataSocket manejoDataSocket , String loginUser)
        {
            String respuestaAEnviar = "";
            String stringOpcionaladmin = "";
            String menus = "";
            try { 
                if(loginUser == null)
                {
                    throw new Exception("Usuario nulo");
                }
               
                if (loginUser.Equals("404"))
                {
                    respuestaAEnviar = "R00404";
                    menus = "Usuario o contraseña incorrectas";
                }
                else 
                {
                    respuestaAEnviar = "R00200";

                    if (loginUser.Equals("200Admin")) { 
                        stringOpcionaladmin = "0. Dar de Alta a un usuario.\n";
                    }
                     menus = "Menu Principal \n"
                               + stringOpcionaladmin +
                            "1. Dar de Alta a un repuesto.\n" +
                            "2. Crear Categoría de repuesto.\n" +
                            "3. Asociar Categorías a un repuesto.\n" +
                            "4. Asociar una foto al repuesto.\n" +
                            "5. Consultar repuestos existentes.\n" +
                            "6. Consultar un repuesto específico.\n" +
                            "7. Enviar y recibir mensajes entre mecánicos.\n" +
                            "8. Configuración.\n" +
                            "9. Exit.";

                }          
                byte[] menu = Encoding.UTF8.GetBytes(menus);
                byte[] menuLargo = BitConverter.GetBytes(menu.Length);
                manejoDataSocket.Send(menuLargo); 
                manejoDataSocket.Send(menu);
            }
            catch(Exception ex)
            {
            }
          
        }
    }
}