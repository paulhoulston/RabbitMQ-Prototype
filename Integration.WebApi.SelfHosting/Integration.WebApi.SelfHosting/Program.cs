using System;

namespace Integration.WebApi.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new RestHost())
            using (var rabbitMQ = new RabbitMQClient())
            {
                new EventHandler(rabbitMQ.Channel);

                Console.WriteLine(" Press [enter] to exit.\r\n\r\n");
                Console.ReadLine();
            }
        }
    }
}