using Newtonsoft.Json;

namespace psCheckPoint.Objects.Service
{
    public class CheckPointService : CheckPointObject
    {
        [JsonConstructor]
        protected CheckPointService(string name, string uID, string type, CheckPointDomain domain,
            string port) :
            base(name, uID, type, domain)
        {
            Port = port;
        }

        [JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Port { get; private set; }
    }
}