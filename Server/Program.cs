using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string IP = "127.0.0.1";
            const int PORT = 8008;
            var endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket clientSocket = null;
            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);


                clientSocket = socket.Accept();
                while (true)
                {
                    int byteCount = 0;
                    byte[] buffer = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                        do
                        {
                            byteCount = clientSocket.Receive(buffer);
                            stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                        } while (clientSocket.Available > 0);
                    string msg = stringBuilder.ToString();
                    if (msg == "end")
                    {
                        clientSocket.Shutdown(SocketShutdown.Send);
                        clientSocket.Close();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"New msg:\t{stringBuilder.ToString()}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            Console.ReadKey();
        }
    }
}
