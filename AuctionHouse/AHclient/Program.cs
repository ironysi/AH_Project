﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AHclient
{
    class Program
    {
        static List<Auction> SimpleAuctions = new List<Auction>();
        static List<Auction> SibscrubedAuctions = new List<Auction>();

        static void Main(string[] args)
        {
            Client testClient = new Client();
            testClient.Run();    
        }
    }
}
