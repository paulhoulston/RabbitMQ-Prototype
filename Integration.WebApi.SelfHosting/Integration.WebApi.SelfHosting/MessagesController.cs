using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Integration.WebApi.SelfHosting
{
    public class MessagesController : ApiController
    {
        public HttpResponseMessage Post(Message message)
        {
            using (var rabbitMQ = new RabbitMQClient())
            {
                rabbitMQ.Channel.BasicPublish(
                    exchange: "",
                    routingKey: "hello",
                    basicProperties: null,
                    body: SerializeMessage(message));
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        static byte[] SerializeMessage(Message message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}