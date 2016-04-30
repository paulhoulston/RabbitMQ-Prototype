using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

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