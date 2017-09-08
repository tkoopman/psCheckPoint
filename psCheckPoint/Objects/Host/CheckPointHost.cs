using Newtonsoft.Json;

namespace psCheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPointHost, Get-CheckPointHost & Get-CheckPointHosts</para>
    /// <para type="description">Host object details.</para>
    /// </summary>
    public class CheckPointHost : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; set; }

        // TODO interfaces

        /// <summary>
        /// <para type="description">IPv4 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", NullValueHandling = NullValueHandling.Ignore)]
        public string ipv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", NullValueHandling = NullValueHandling.Ignore)]
        public string ipv6Address { get; set; }

        //TODO nat-settings
        //TODO host-servers
    }
}