using System;
using System.Collections.Generic;
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

        public void SendToServer(string text)
        {
            server.Writer.WriteLine(text);
            server.Writer.Flush();
        }

        public void Listener()
        {
            bool keepGoing = true;

            while (keepGoing == true)
            {
                string text;
                text = RecieveFromServer();
                if (text == null)
                {
                    keepGoing = false;
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }

    }
}
