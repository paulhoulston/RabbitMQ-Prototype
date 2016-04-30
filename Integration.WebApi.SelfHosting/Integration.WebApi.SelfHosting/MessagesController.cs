using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Integration.WebApi.SelfHosting
{
    public class MessagesController : ApiController
    {
        public HttpResponseMessage Post([FromBody]Message message)
        {
            using (var rabbitMQ = new RabbitMQClient())
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                rabbitMQ.Channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}