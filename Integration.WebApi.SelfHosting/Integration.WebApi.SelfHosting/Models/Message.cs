namespace Integration.WebApi.SelfHosting.Models
{
    public class Message
    {
        public string EventType { get; set; }
        public dynamic MetaData { get; set; }
        public dynamic Data { get; set; }
    }
}