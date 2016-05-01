using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Integration.WebApi.SelfHosting.Configuration
{
    public class EventsMappingSection : ConfigurationSection
    {
        public class MappedEvent
        {
            public string Script { get; set; }
            public TypeOfScript ScriptType { get; set; }
        }

        public static IDictionary<string, MappedEvent> MappedEvents = ((EventsMappingSection)ConfigurationManager.GetSection("eventTypeMapping")).Events.Cast<EventElement>().ToDictionary(ev => ev.EventType, ev => new MappedEvent { ScriptType = ev.ScriptType, Script = Path.Combine(((EventsMappingSection)ConfigurationManager.GetSection("eventTypeMapping")).ScriptsFolder, ev.Script) });

        [ConfigurationProperty("events", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EventsCollection), AddItemName = "add")]
        public EventsCollection Events
        {
            get { return (EventsCollection)base["events"]; }
        }

        [ConfigurationProperty("scriptsFolder", IsRequired = false, DefaultValue = "")]
        public string ScriptsFolder
        {
            get { return (string)this["scriptsFolder"]; }
            set { this["scriptsFolder"] = value; }
        }
    }
}