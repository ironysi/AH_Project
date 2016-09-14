using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public static string JsonSerialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static CommunicationData Decode(string json)
        {
            return JsonConvert.DeserializeObject<CommunicationData>(json);
        }

    }
}
