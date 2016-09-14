using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Server
    {
        private IPAddress Ip;
        private int Port;
        private volatile bool Stop = false;
        public static readonly int MaxClients = 10;
        public static int CurrentClients = 0;
        public static readonly object _object = new object();

        public Server(string ip, int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
        }

        public Server(int port) : this ("127.0.0.1", port){ }

        public void Run()
        {
            Console.WriteLine("Starting up server on " + Ip + ":" + Port);
            TcpListener listener = new TcpListener(Ip, Port);
            listener.Start();

            

            while (!Stop)
            {
                Socket clientSocket = listener.AcceptSocket();

                Console.WriteLine("New connection incoming from: " + clientSocket.LocalEndPoint);


                ClientHandler handler = new ClientHandler(clientSocket);

                Thread clientThread = new Thread(handler.Run);

                clientThread.Start();            
            }
        }
    }
}
