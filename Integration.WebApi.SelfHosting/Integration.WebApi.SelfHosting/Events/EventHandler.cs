using System;
using System.Collections.Generic;
using System.IO;
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
        static readonly IDictionary<TypeOfScript, IExecuteScripts> _scriptExecutors = new Dictionary<TypeOfScript, IExecuteScripts>
        {
            {TypeOfScript.PowerShell, new ExecutePowerShellScript() }
        };

        public EventHandler(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleEvent;
            channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);
        }

        static void HandleEvent(object _, BasicDeliverEventArgs ea)
        {
            var message = DeserializeEvent(ea);
            var eventMapping = GetEventScriptHandler(message);

            if (eventMapping == null)
            {
                Console.WriteLine("No script defined to handle event '{0}'", message.EventType);
                return;
            }

            Console.WriteLine("Executing script '{1}' [{2}] for event '{0}'", eventMapping.EventType, eventMapping.Script, eventMapping.EventType);
            _scriptExecutors[eventMapping.ScriptType].Execute(eventMapping.Script, message.Data);
        }

        static Message DeserializeEvent(BasicDeliverEventArgs ea)
        {
            return JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(ea.Body));
        }

        static EventElement.IAmAnEventMapping GetEventScriptHandler(Message eventObj)
        {
            return
                EventsMappingSection
                    .Settings
                    .Events
                    .Cast<EventElement.IAmAnEventMapping>()
                    .SingleOrDefault(evnt => evnt.EventType.Equals(eventObj.EventType));
        }
    }
}