using System;
using System.Text;
using RabbitMQ.Client;
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
                new EventHandler(rabbitMQ.Channel);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }

    class EventHandler
    {
        public EventHandler(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleEvent;
            channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);
        }

        private static void HandleEvent(object model, BasicDeliverEventArgs ea)
        {
            Console.WriteLine(" [x] Received {0}", Encoding.UTF8.GetString(ea.Body));
        }
    }
}