using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Details of a Check Point Domain</para>
    /// </summary>
    public class CheckPointDomain
    {
        [JsonConstructor]
        private CheckPointDomain(string name, string uID, string domainType)
        {
            Name = name;
            UID = uID;
            DomainType = domainType;
        }

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public string Name { get; private set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">Domain type.</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain-type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        public string DomainType { get; private set; }

        /// <summary>
        /// <para type="description">Convert domain object to string. (Domain name)</para>
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}