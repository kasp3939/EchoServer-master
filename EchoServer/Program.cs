using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Program P = new Program();
            P.Server();
        }

        public void Server()
        {
            
            TcpListener listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();
            Console.WriteLine("Ready");
            Socket client = listener.AcceptSocket();
            NetworkStream stream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);

            while (client.Connected)
            {
                string receivedMessage = reader.ReadLine();
                Console.WriteLine("Message from client " + client.LocalEndPoint);
                Console.WriteLine(receivedMessage);

                
            }
            Console.ReadKey();
            client.Close();
        }
    }
}
