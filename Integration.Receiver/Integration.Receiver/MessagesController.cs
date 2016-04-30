using System.Web.Http;

namespace Integration.Receiver
{
    public class MessagesController : ApiController
    {
        public string Get()
        {
            return "Test";
        }
    }
}