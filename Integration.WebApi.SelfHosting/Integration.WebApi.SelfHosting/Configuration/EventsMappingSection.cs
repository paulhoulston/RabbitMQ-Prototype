using System.Configuration;

namespace Integration.WebApi.SelfHosting.Configuration
{
    public class EventsMappingSection : ConfigurationSection, EventsMappingSection.IContainEventMappings
    {
        public interface IContainEventMappings
        {
            EventsCollection Events { get; }
        }

        public static IContainEventMappings Settings = (IContainEventMappings)ConfigurationManager.GetSection("eventTypeMapping");

        [ConfigurationProperty("events", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EventsCollection), AddItemName = "add")]
        public EventsCollection Events
        {
            get { return (EventsCollection)base["events"]; }
        }
    }
}