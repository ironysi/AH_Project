using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace AHclient
{
    class ServerConnection
    {
        public string ServerName { get; set; }
        public int Port { get; set; }
        public TcpClient Server { get; set; }
        public NetworkStream Stream { get; set; }
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }

        public ServerConnection(string servername, int port)
        {
            this.ServerName = servername;
            this.Port = port;
        }

        public void Connect()
        {
            Console.WriteLine("Connecting to server "+ ServerName + ", on port " + Port);

            Server = new TcpClient(ServerName, Port);
            Stream = Server.GetStream();
            Reader = new StreamReader(Stream);
            Writer = new StreamWriter(Stream);
        }

        public void CloseConnection()
        {
            Server.Close();
            Stream.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}
