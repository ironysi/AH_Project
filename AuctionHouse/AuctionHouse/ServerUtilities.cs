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
        // List containing all the auctions running in the system.
        public static List<ServerAuction> AuctionList = new List<ServerAuction>();
        public static List<ClientHandler> ClientList = new List<ClientHandler>();

        public static int Time { get; set; }        

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
            CommunicationData data = new CommunicationData();
            data = JsonConvert.DeserializeObject<CommunicationData>(json);
            return data;
        }

        public static void RemoveClient(int id)
        {
            if (ClientList.Count > 0)
            {
                int index = ClientList.FindIndex(c => c.Id == id);
                if (index != -1)
                {
                    ClientList.RemoveAt(index);
                }
            }
        }

    }
}
