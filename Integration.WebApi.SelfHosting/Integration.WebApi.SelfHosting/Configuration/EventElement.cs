using System.Configuration;

namespace Integration.WebApi.SelfHosting.Configuration
{
    public class EventElement : ConfigurationElement
    {
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

        [ConfigurationProperty("scriptType", DefaultValue = TypeOfScript.PowerShell)]
        public TypeOfScript ScriptType
        {
            get { return (TypeOfScript)this["scriptType"]; }
            set { this["scriptType"] = value; }
        }
    }
}