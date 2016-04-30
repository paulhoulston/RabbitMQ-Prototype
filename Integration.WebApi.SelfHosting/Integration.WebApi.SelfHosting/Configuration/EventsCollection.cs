using System.Configuration;

namespace Integration.WebApi.SelfHosting.Configuration
{
    public class EventsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EventElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EventElement)element).EventType;
        }
    }
}