
using Protocolo;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cliente2
{
    internal class ProgramClienteII
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Cliente");

            // 1- Se crea el Socket del cliente
            var socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 2- Definimos un Endpoint local del cliente
            var endpointCliente = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
            // 3- Asociación entre el socket y el endpoint local
            socketCliente.Bind(endpointCliente);

            // 4- Definir un Endpoint remoto con la Ip y el puerto que tenga el servidor. Debo conocer los datos a los que me voy a conectar
            var endpointServidor = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);

            // 5- Establezco conexión con el socket y el endpoint remoto (Servidor)
            socketCliente.Connect(endpointServidor);
            Console.WriteLine("Conexión establecida");

            Console.WriteLine("Escriba su Usuario#Contrasena");
            string usuario = Console.ReadLine();
           
            bool exit = false;
            ManejoDataSocket manejoDataSocket = new ManejoDataSocket(socketCliente);
            byte[] datosLargoMenu = manejoDataSocket.Receive(Constantes.LargoFijo);
            byte[] datosMenu = manejoDataSocket.Receive(BitConverter.ToInt32(datosLargoMenu));

            string mensajeMenu = $"{Encoding.UTF8.GetString(datosMenu)}";
            Console.WriteLine(mensajeMenu);
            while (!exit)
            {
                string mensaje = Console.ReadLine();
                if (mensaje.Equals("exit"))
                {
                    exit = true;
                }
                else
                {
                    byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                    byte[] datosLargo = BitConverter.GetBytes(datos.Length);

                    try
                    {
                        manejoDataSocket.Send(datosLargo); // Mando la parte fija (4 bytes)
                        manejoDataSocket.Send(datos);
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("La conexión con el servidor se ha cortado");
                        exit = true;
                    }
                   Console.WriteLine(mensajeMenu);
                }
            }

            // 6- Se indica que no quiero tener más flujo de datos
            socketCliente.Shutdown(SocketShutdown.Both); //Implica que no quiero recibir ni enviar mas datos
            // 7- Cierro la conexión
            socketCliente.Close();
        }
    }
}