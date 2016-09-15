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
        public TcpClient Server;
        public NetworkStream Stream;
        public StreamReader Reader;
        public StreamWriter Writer;

        public ServerConnection(string servername, int port)
        {
            this.ServerName = servername;
            this.Port = port;
        }

        public bool Connect()
        {
            Console.WriteLine("Connecting to server "+ ServerName + ", on port " + Port);
            bool result = false;
            try
            {
                Server = new TcpClient(ServerName, Port);
                Stream = Server.GetStream();
                Reader = new StreamReader(Stream);
                Writer = new StreamWriter(Stream);
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return result;
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
