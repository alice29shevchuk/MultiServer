using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";
            const int PORT = 8008;
            var endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(endPoint);
                while (true)
                {
                    Console.WriteLine("Введите текст: ");
                    string mes = Console.ReadLine();
                        var buffer = Encoding.Unicode.GetBytes(mes);
                        socket.Send(buffer);
                    if (mes.Equals("end"))
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        Environment.Exit(0);
                    }
                }
            }
            catch { }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            Console.ReadLine();
        }
    }
}
