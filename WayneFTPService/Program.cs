using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WayneFTPService
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello world");

            var start = new FTPService();
            var result = start.ListAllFiles();

            Console.WriteLine(result);

            using (StreamReader file = File.OpenText("FTPConfig/config.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject) JToken.ReadFrom(reader);
                Console.WriteLine(o2.ToString());
            }
        }
    }
}