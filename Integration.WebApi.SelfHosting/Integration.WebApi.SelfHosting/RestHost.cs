using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Integration.WebApi.SelfHosting
{
    class RestHost : IDisposable
    {
        readonly IDisposable _webapp;

        public RestHost()
        {
            _webapp = WebApp.Start<Startup>(url: GetBaseAddress());
        }

        public void Dispose()
        {
            if (_webapp != null)
                _webapp.Dispose();
        }

        static string GetBaseAddress()
        {
            return string.Format(
                            "http://{0}:{1}/",
                            ConfigurationManager.AppSettings["HTTP_LISTENING_HOST"],
                            ConfigurationManager.AppSettings["HTTP_LISTENING_PORT"]);
        }
    }
}