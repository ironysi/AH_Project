using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class ClientHandler
    {
        private Socket ClientSocket;
        private NetworkStream NetStream;
        private StreamWriter Writer;
        private StreamReader Reader;

        public ClientHandler(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }

        public void Run()
        {
            NetStream = new NetworkStream(ClientSocket);
            Writer = new StreamWriter(NetStream);
            Reader = new StreamReader(NetStream);

            Communicate();

            Close();

        }

        public void Close()
        {
            Writer.Close();
            Reader.Close();
            NetStream.Close();
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
        }

        private string ReciveFromClient()
        {
            try
            {
                return Reader.ReadLine();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SendToClient(string text)
        {
            Writer.WriteLine(text);
            Writer.Flush();
        }

        private void Communicate()
        {
            
        }



    }

    
}
