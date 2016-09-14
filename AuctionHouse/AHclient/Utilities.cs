using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
