using System;
using System.Linq;

namespace SmartModemReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 && args.Length != 2)
            {
                Console.WriteLine("Smart modem reader v1");
                Console.WriteLine("Usage:");
                Console.WriteLine("1) Pass the IP address as first argument and the session ID as second.");
                Console.WriteLine("2) To use the default address (192.168.1.1) pass only the session ID as first argument");
            }
            else if (args.Length == 1)
            {
                RunReader(sessionId: args[0]);
            }
            else if (args.Length == 2)
            {
                RunReader(ip: args[0], sessionId: args[1]);
            }
        }

        static void RunReader(string sessionId, string ip = "192.168.1.1")
        {
            var reader = new DslDataReader(sessionId, ip);

            var data = reader.ReadDataAsync().Result;
            // var data = reader.ReadSampleDataAsync().Result;

            Console.WriteLine(data.ToPrettyString());
        }
    }
}
