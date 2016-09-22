using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace AHclient
{
    class Client
    {
        private ServerConnection server = new ServerConnection("127.0.0.1", 12000);

        public void Run()
        {
            Thread.Sleep(3000);
            bool connectionResult = server.Connect();

            if (connectionResult)
            {
                Thread listenerThread = new Thread(Listener);
                listenerThread.Start();

                Console.Write("Please enter your name: ");
                string inputName = Console.ReadLine();
                while (string.IsNullOrEmpty(inputName) || string.IsNullOrWhiteSpace(inputName))
                {
                    Console.WriteLine("Invalid name.");
                    Console.Write("Please enter your name again: ");
                    inputName = Console.ReadLine();
                }

                SendToServer(new CommunicationData("SetClientName", inputName).Encode());

                Communicate();

                server.CloseConnection();
            }
            else
            {
                Console.ReadLine();
            }
            
        }

        public string RecieveFromServer()
        {
            try
            {
                return server.Reader.ReadLine();
            }
            catch
            {
                return null;
            }
        }

        public void SendToServer(string data)
        {
            server.Writer.WriteLine(data);
            server.Writer.Flush();
        }

        public void Listener()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                string jsonData = RecieveFromServer();
                if (jsonData == null)
                {
                    keepGoing = false;
                }
                else
                {
                    CommunicationData recivedData = Utilities.Decode(jsonData);
                    switch (recivedData.Action)
                    {
                        case "OutPutMessage":
                            Console.WriteLine(recivedData.Data);
                            break;
                        case "AddAuctionToList":
                            Auction auction = JsonConvert.DeserializeObject<Auction>(recivedData.Data);
                            if (Utilities.AuctionList.Count > 0)
                            {
                                int index = Utilities.AuctionList.FindIndex(a => a.ID == auction.ID);
                                if (index != -1)
                                {
                                    Utilities.AuctionList[index] = auction;
                                }
                                else
                                {
                                    Utilities.AuctionList.Add(auction);
                                }
                            }
                            break;
                        default:
                            Debug.WriteLine("Recived invalid action.");
                            break;

                    }
                }
            }
        }

        private void Communicate()
        {
            while (Execute()) ;
        }

        private bool Execute()
        {
            string command = Console.ReadLine();

            if (command[0] == 'b' && command[1] == 'i' && command[2] == 'd' && command[3] == ' ')
            {
                string input = "";
                for (int i = 4; i < command.Length; i++)
                {
                    input += command[i];
                }

                int x = 0;

                if(int.TryParse(input, out x))
                {
                    SendToServer(new CommunicationData("Bid", input).Encode());

                }else
                {
                    Console.WriteLine("Invalid Syntax");
                }

            }
            else
            {
                SendToServer(new CommunicationData("ExecuteCommand", command).Encode());
                switch (command.Trim().ToLower())
                {
                    case "exit":
                        return false;
                }
            }
                return true;

        }

    }
}
