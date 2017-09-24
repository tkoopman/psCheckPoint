using Newtonsoft.Json;

namespace psCheckPoint.Objects.Service
{
    /// <summary>
    ///
    /// </summary>
    public class CheckPointService : CheckPointObject
    {
        /// <summary>
        /// JSON Constructor for Check Point Service Summary
        /// </summary>
        [JsonConstructor]
        protected CheckPointService(string name, string uID, string type, CheckPointDomain domain,
            string port) :
            base(name, uID, type, domain)
        {
            Port = port;
        }

        /// <summary>
        /// <para type="description">The number of the port used to provide this service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Port { get; private set; }
    }
}