using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AHclient
{
     public static class Utilities
    {
        public static string JsonSerialize<T>(T obj)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return json;
        }

        //public static T JasonDeserialize<T>(string json, Type classType)
        //{
        //    var b = (Auction)Newtonsoft.Json.JsonConvert.DeserializeObject(json);

        //}


         public static CommunicationData Decode(string json)
         {
             return JsonConvert.DeserializeObject<CommunicationData>(json);
         }

    }
}
