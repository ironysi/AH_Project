using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AHclient
{
    class Client
    {
        private ServerConnection server = new ServerConnection("127.0.0.1", 12000);

        public void Run()
        {
            server.Connect();

            Thread listenerThread = new Thread(Listener);
            listenerThread.Start();

            Communicate();

            server.CloseConnection();
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
                    CommunicationData data = Utilities.Decode(jsonData);
                    switch (data.Action)
                    {
                        case "OutPutMessage":
                            Console.WriteLine(data.Data);
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
            SendToServer(new CommunicationData("ExecuteCommand", command).Encode());
            switch (command.Trim().ToLower())
            {
                case "exit":
                    return false;
            }
            return true;
        }

    }
}
