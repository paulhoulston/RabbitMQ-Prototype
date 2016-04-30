using System;
using System.Text;
using RabbitMQ.Client.Events;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new RestHost())
            using (var rabbitMQ = new RabbitMQClient())
            {
                var consumer = new EventingBasicConsumer(rabbitMQ.Channel);
                consumer.Received += HandleEvent;
                rabbitMQ.Channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void HandleEvent(object model, BasicDeliverEventArgs ea)
        {
            Console.WriteLine(" [x] Received {0}", Encoding.UTF8.GetString(ea.Body));
        }
    }

}