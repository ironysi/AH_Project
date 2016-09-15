using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AHclient
{
    public static class Utilities
    {
        public static List<Auction> AuctionList = new List<Auction>();
        public static string JsonSerialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static CommunicationData Decode(string json)
        {
            CommunicationData data = new CommunicationData();
            data = JsonConvert.DeserializeObject<CommunicationData>(json);
            return data;
        }
    }
}
