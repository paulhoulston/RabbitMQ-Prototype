using System;
using System.Text;
using Integration.WebApi.SelfHosting.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Integration.WebApi.SelfHosting.Events
{
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
            var message = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(ea.Body));

            Console.WriteLine("Received event type '{0}'", message.EventType);
        }
    }
}