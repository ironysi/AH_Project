using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse
{
    public static class ServerUtilities
    {
        static public int Time { get; set; }
        public static void Clock()
        {
            while (true)
            {
                Time++;
                Thread.Sleep(1000);
            }
        }

        static public void RunAuction()
        {
            
        }

    }
}
