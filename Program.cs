using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
                Console.WriteLine("3) To use the web app, pass 'web' as first argument followed by ip='ipaddress' (optional) and sid='sessionid'");
            }
            else if (args.Length > 0 && args[0] == "web")
            {
                BuildWebHost(args.Skip(1).ToArray()).Run();
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

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
