using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuctionHouse
{
    public class CommunicationData
    {
        public string Action { get; set; }
        public string Data { get; set; }
        public bool IsDataJson { get; set; }

        public CommunicationData(string action, string data, bool isDataJson = false)
        {
            Action = action;
            Data = data;
            IsDataJson = isDataJson;
        }

        public CommunicationData()
        {
            
        }

        public string Encode()
        {
            return Utilities.JsonSerialize(this);
        }

    }
}
