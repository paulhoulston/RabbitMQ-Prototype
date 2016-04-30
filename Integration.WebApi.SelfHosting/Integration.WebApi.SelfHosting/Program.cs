using System;
using System.Configuration;
using System.Text;
using Microsoft.Owin.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = ConfigurationManager.AppSettings["RABBITMQ_HOST"] };

            using (var webapp = WebApp.Start<Startup>(url: GetBaseAddress()))
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    Console.WriteLine(" [x] Received {0}", Encoding.UTF8.GetString(ea.Body));
                };
                channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

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