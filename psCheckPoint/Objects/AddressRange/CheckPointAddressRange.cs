using Newtonsoft.Json;

namespace psCheckPoint.Objects.AddressRange
{
    /// <summary>
    /// <para type="description">Details of a Check Point Address Range</para>
    /// </summary>
    public class CheckPointAddressRange : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; set; }

        /// <summary>
        /// <para type="description">First IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-first", NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-last", NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4AddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-first", NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-last", NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6AddressLast { get; set; }

        //TODO nat-settings
    }
}