using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Integration.WebApi.SelfHosting
{
    public class MessagesController : ApiController
    {
        public HttpResponseMessage Post([FromBody]Message message)
        {
            var factory = new ConnectionFactory() { HostName = ConfigurationManager.AppSettings["RABBITMQ_HOST"] };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}