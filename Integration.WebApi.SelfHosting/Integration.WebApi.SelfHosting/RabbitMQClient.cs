using System;
using System.Configuration;
using RabbitMQ.Client;

namespace Integration.WebApi.SelfHosting
{
    class RabbitMQClient : IDisposable
    {
        public readonly IModel Channel;
        private readonly IConnection _connection;

        public RabbitMQClient()
        {
            var factory = new ConnectionFactory() { HostName = ConfigurationManager.AppSettings["RABBITMQ_HOST"] };
            _connection = factory.CreateConnection();
            Channel = _connection.CreateModel();
            Channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Dispose()
        {
            if (Channel != null)
                Channel.Dispose();
            if (_connection != null)
                _connection.Dispose();
        }
    }
}