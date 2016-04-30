using System;
using System.Configuration;
using System.Text;
using Microsoft.Owin.Hosting;
using RabbitMQ.Client.Events;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webapp = WebApp.Start<Startup>(url: GetBaseAddress()))
            using (var rabbitMQ = new RabbitMQClient())
            {
                var consumer = new EventingBasicConsumer(rabbitMQ.Channel);
                consumer.Received += (model, ea) =>
                {
                    Console.WriteLine(" [x] Received {0}", Encoding.UTF8.GetString(ea.Body));
                };
                rabbitMQ.Channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

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