using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHclient
{
     public static class Utilities
    {
         public static string JsonSerialize<T>(T obj)
         {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return json;
         }

    }
}
