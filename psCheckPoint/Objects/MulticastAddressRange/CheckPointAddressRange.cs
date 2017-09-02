using Newtonsoft.Json;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPointMulticastAddressRange, Get-CheckPointMulticastAddressRange & Get-CheckPointMulticastAddressRanges</para>
    /// <para type="description">Multicast Address Range object details.</para>
    /// </summary>
    public class CheckPointMulticastAddressRange : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public CheckPointObject[] Groups { get; set; }

        /// <summary>
        /// <para type="description">First IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-first")]
        public string IPv4AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-last")]
        public string IPv4AddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-first")]
        public string IPv6AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-last")]
        public string IPv6AddressLast { get; set; }
    }
}