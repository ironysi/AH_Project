using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Server
    {
        private IPAddress ip;
        private int port;
        private volatile bool stop = false;

        public Server(string ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
        }

        public Server(int port) : this ("127.0.0.1", port){ }

        public void Run()
        {
            Console.WriteLine("Starting up server on " + ip + ":" + port);
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            while (!stop)
            {
                Socket clientSocket = listener.AcceptSocket();

                Console.WriteLine("New connection incoming from: " + clientSocket.LocalEndPoint);
            }
        }
    }
}
