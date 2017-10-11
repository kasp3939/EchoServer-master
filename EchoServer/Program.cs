using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace EchoServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program _P = new Program();
            _P.Run();
        }

        List<StreamWriter> listOfClients;
        List<Socket> listOfClientsSockets;

        private void Run()
        {
            listOfClients = new List<StreamWriter>();
            listOfClientsSockets = new List<Socket>();
            TcpListener listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();
            Console.WriteLine("Ready"); 
            while (true)
            {
                Socket client = listener.AcceptSocket();
                listOfClientsSockets.Add(client);
                Thread threadServer = new Thread(Server);
                threadServer.Start(client);
            }
            
        }
        public void Server(object obj)
        {
            Socket client = (Socket)obj;
                         
            NetworkStream stream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);

            listOfClients.Add(writer);

            while (client.Connected)
            {
                string data = reader.ReadLine();
                string[] input = data.Split(' ');
                int x = int.Parse(input[1]);
                int y = int.Parse(input[2]);

                switch (input[0])
                {
                    case "add":
                        int add = x + y;
                        Console.WriteLine("Sum " + add); break;

                    case "minus":
                        int minus = x - y;
                        Console.WriteLine("Sum " + minus); break;
                    case "diff":
                        int diff = x / y;
                        Console.WriteLine("Sum " + diff); break;
                    case "exit":
                        client.Close(); break;
                    default:
                        break;

                }
                client.Close();
            }
        }
    }
}

//PLUS MINUS OG DIVIDERE
//string data = reader.ReadLine();
//string[] input = data.Split(' ');
//int x = int.Parse(input[1]);
//int y = int.Parse(input[2]);

//switch (input[0])
//{ 
//case "add":
//                        int add = x + y;
//Console.WriteLine("Sum " + add); break;

//                    case "minus":
//                        int minus = x - y;
//Console.WriteLine("Sum " + minus); break;
//                    case "diff":
//                        int diff = x / y;
//Console.WriteLine("Sum " + diff); break;
//                    case "exit":
//                        client.Close(); break;
//                    default:
//                        break;


//SKRIVER HVAD CLIENT SENDER
//string receivedMessage = reader.ReadLine();
//Console.WriteLine("Message from client " + client.LocalEndPoint);
//Console.WriteLine(receivedMessage);



//SKRIVER TID OG DATO MED "date"
//string receivedMessage = reader.ReadLine();

//DateTime date = DateTime.Now;

//if (receivedMessage == "date")
//{
//Console.WriteLine(date.ToString());
//}
//else
//{
//Console.WriteLine("Unknown Command from " + client.LocalEndPoint);
//}