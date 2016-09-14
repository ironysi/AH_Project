using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public void Run()
        {
            Server server = new Server(12000);

            Thread listeningForConnections = new Thread(server.Run);
            listeningForConnections.Start();

            Console.ReadLine();
           
        }


    }
}
