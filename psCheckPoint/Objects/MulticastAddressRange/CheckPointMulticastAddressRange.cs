using Newtonsoft.Json;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <summary>
    /// <para type="description">Details of a Check Point Multicast Address Range</para>
    /// </summary>
    public class CheckPointMulticastAddressRange : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointMulticastAddressRange(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, string pv4AddressFirst, string pv4AddressLast, string pv6AddressFirst, string pv6AddressLast) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            IPv4AddressFirst = pv4AddressFirst;
            IPv4AddressLast = pv4AddressLast;
            IPv6AddressFirst = pv6AddressFirst;
            IPv6AddressLast = pv6AddressLast;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        /// <summary>
        /// <para type="description">First IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-first", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4AddressFirst { get; private set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-last", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4AddressLast { get; private set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-first", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6AddressFirst { get; private set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-last", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6AddressLast { get; private set; }

        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }
    }
}