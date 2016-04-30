using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webapp = WebApp.Start<Startup>(url: GetBaseAddress()))
            {
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static string GetBaseAddress()
        {
            return string.Format(
                            "http://{0}:{1}/",
                            ConfigurationManager.AppSettings["HTTP_LISTENING_HOST"],
                            ConfigurationManager.AppSettings["HTTP_LISTENING_PORT"]);
        }
    }
}