using System.Configuration;

namespace Integration.WebApi.SelfHosting.Configuration
{
    public class EventElement : ConfigurationElement, EventElement.IAmAnEventMapping
    {
        public interface IAmAnEventMapping
        {
            string EventType { get; }
            string Script { get; }
        }

        [ConfigurationProperty("eventType", IsRequired = true, IsKey = true)]
        public string EventType
        {
            get { return (string)this["eventType"]; }
            set { this["eventType"] = value; }
        }

        [ConfigurationProperty("script", IsRequired = true, IsKey = false)]
        public string Script
        {
            get { return (string)this["script"]; }
            set { this["script"] = value; }
        }
    }
}