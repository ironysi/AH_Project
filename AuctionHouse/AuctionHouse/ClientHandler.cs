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
    public class ClientHandler:IClient
    {
        private Socket ClientSocket;
        private NetworkStream NetStream;
        private StreamWriter Writer;
        private StreamReader Reader;

        public int Id { get; set; }
        public string Name { get; set; }

        public ClientHandler(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }

        public void Run()
        {
            NetStream = new NetworkStream(ClientSocket);
            Writer = new StreamWriter(NetStream);
            Reader = new StreamReader(NetStream);

            if (Server.CurrentClients < Server.MaxClients)
            {
                Monitor.Enter(Server._object);
                try
                {
                    Server.CurrentClients++;
                    Server.ClientIdCounter++;
                    Id = Server.ClientIdCounter;
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
            ServerUtilities.RemoveClient(Id);
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
            try
            {
                Writer.WriteLine(data);
                Writer.Flush();
            }
            catch (Exception e)
            {
                ServerUtilities.RemoveClient(Id);
            }
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
                        case "updateclientstest":
                            ServerUtilities.AuctionList[0].Notify();
                            break;
                        default:
                            SendToClient(new CommunicationData("OutPutMessage", "Invalid command.").Encode());
                            break;
                    }
                    break;
                case "SetClientName":
                    Name = recivedData.Data;
                    SendToClient(new CommunicationData("OutPutMessage", "You are signed in as - " + recivedData.Data + Environment.NewLine + "To exit type: exit").Encode());
                    break;
                default:
                    Console.WriteLine("Invalid action recived.");
                    break;

            } 
            return true;
        }

        public void Update()
        {
            SendToClient(new CommunicationData("OutPutMessage", "Hi this is update for Client - Name: " + Name + " Id: " + Id).Encode());
        }
    }


}