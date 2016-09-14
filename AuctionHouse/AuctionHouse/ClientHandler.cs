using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
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

            SendToClient(new CommunicationData("OutPutMessage", "Sup dude, welkomen!").Encode());

            if (Server.CurrentClients < Server.MaxClients)
            {
                Monitor.Enter(Server._object);
                try
                {
                    Server.CurrentClients++;
                }
                finally
                {
                    Monitor.Exit(Server._object);
                }
                Communicate();
            }
            else
            {
                SendToClient(new CommunicationData("OutPutMessage", "Server is full. Disconnecting...").Encode());
                Close();
            }           
        }

        public void Close()
        {
            Writer.Close();
            Reader.Close();
            NetStream.Close();
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();

            Monitor.Enter(Server._object);
            try
            {
                Server.CurrentClients--;
            }
            finally
            {
                Monitor.Exit(Server._object);
            }
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

        private void SendToClient(string data)
        {
            Writer.WriteLine(data);
            Writer.Flush();
        }

        private void Communicate()
        {
            while (Execute()) ;
        }

        private bool Execute()
        {
            string jsonData = ReciveFromClient();
            Debug.WriteLine(jsonData);
            if (jsonData == null)
                return false;

            CommunicationData recivedData = ServerUtilities.Decode(jsonData);
            switch (recivedData.Action)
            {
                case "ExecuteCommand":
                    switch (recivedData.Data.Trim().ToLower())
                    {
                        case "exit":
                            Close();
                            return false;
                        case "servertime":
                            int serverTime = ServerUtilities.Time;
                            SendToClient(new CommunicationData("OutPutMessage", "Server time: " + serverTime).Encode());
                            break;
                        default:
                            SendToClient(new CommunicationData("OutPutMessage", "Invalid command.").Encode());
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid action recived.");
                    break;

            } 
            return true;
        }
    }


}