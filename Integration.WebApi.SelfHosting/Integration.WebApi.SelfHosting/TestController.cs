using System.Web.Http;

namespace Integration.WebApi.SelfHosting
{

    public class TestController : ApiController
    {
        public string Get()
        {
            return "Hello world";
        }
    }

}