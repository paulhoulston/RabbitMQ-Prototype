using System;
using System.Linq;
using System.Text;
using Integration.WebApi.SelfHosting.Configuration;
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

            var eventMapping =
                EventsMappingSection
                    .Settings
                    .Events
                    .Cast<EventElement.IAmAnEventMapping>()
                    .SingleOrDefault(evnt => evnt.EventType.Equals(message.EventType));


            Console.WriteLine("Received event type '{0}', run script '{1}'", message.EventType, eventMapping == null ? "not found" : eventMapping.Script);
        }
    }
}