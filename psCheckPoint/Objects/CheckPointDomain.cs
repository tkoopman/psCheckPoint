using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    public class CheckPointDomain
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Domain type.</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain-type")]
        public string DomainType { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}