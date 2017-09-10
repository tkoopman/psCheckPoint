using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Details of a Check Point Domain</para>
    /// </summary>
    public class CheckPointDomain
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Domain type.</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain-type", NullValueHandling = NullValueHandling.Ignore)]
        public string DomainType { get; set; }

        /// <summary>
        /// <para type="description">Convert domain object to string. (Domain name)</para>
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}