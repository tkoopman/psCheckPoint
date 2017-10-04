using Newtonsoft.Json;

namespace psCheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="description">Details of a Check Point Host</para>
    /// </summary>
    public class CheckPointHost : CheckPointObjectFull
    {
        /// <summary>
        /// JSON Constructor for Host
        /// </summary>
        [JsonConstructor]
        private CheckPointHost(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, string ipv4Address, string ipv6Address) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            IPv4Address = ipv4Address;
            IPv6Address = ipv6Address;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "interfaces", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Interface[] Interfaces { get; private set; }

        /// <summary>
        /// <para type="description">Host interfaces.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4Address { get; private set; }

        /// <summary>
        /// <para type="description">IPv6 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6Address { get; private set; }

        //TODO nat-settings
        //TODO host-servers

        /// <summary>
        /// Conditional Property Serialization for Groups
        /// </summary>
        /// <returns>true if Groups should be serialised.</returns>
        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }

        /// <summary>
        /// <para type="description">Details of a Check Point Host Interface</para>
        /// </summary>
        public class Interface : CheckPointObjectFull
        {
            [JsonConstructor]
            private Interface(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
                string subnet4, string subnet6, int maskLength4, int maskLength6, string subnetMask) :
                base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
            {
                Subnet4 = subnet4;
                Subnet6 = subnet6;
                MaskLength4 = maskLength4;
                MaskLength6 = maskLength6;
                SubnetMask = subnetMask;
            }

            [JsonProperty(PropertyName = "subnet4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            public string Subnet4 { get; private set; }

            [JsonProperty(PropertyName = "subnet6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            public string Subnet6 { get; private set; }

            [JsonProperty(PropertyName = "mask-length4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            public int MaskLength4 { get; private set; }

            [JsonProperty(PropertyName = "mask-length6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            public int MaskLength6 { get; private set; }

            [JsonProperty(PropertyName = "subnet-mask", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            public string SubnetMask { get; private set; }
        }
    }
}