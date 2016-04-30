using System;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            var restHost = new RestHost();
            var rabbitMQ = new RabbitMQClient();

            try
            {
                new Events.EventHandler(rabbitMQ.Channel);
            }
            catch (Exception ex)
            {
                var inner = ex;
                while (inner != null)
                {
                    Console.WriteLine(inner.Message);
                    inner = inner.InnerException;
                }
            }
            finally
            {
                Console.WriteLine(" Press [enter] to exit.\r\n\r\n");
                Console.ReadLine();

                if (rabbitMQ != null)
                    rabbitMQ.Dispose();
                if (restHost != null)
                    restHost.Dispose();
            }
        }
    }
}