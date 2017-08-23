using Newtonsoft.Json;

namespace CheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPointHost, Get-CheckPointHost & Get-CheckPointHosts</para>
    /// <para type="description">Host object details.</para>
    /// </summary>
    public class CheckPointHost : CheckPointObject
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public CheckPointObject[] Groups { get; set; }

        /// <summary>
        /// <para type="description">Object icon.</para>
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        // TODO interfaces

        /// <summary>
        /// <para type="description">IPv4 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address")]
        public string ipv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address")]
        public string ipv6Address { get; set; }

        //TODO meta-info
        //TODO nat-settings

        /// <summary>
        /// <para type="description">Indicates whether the object is read-only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only")]
        public bool ReadOnly { get; set; }

        //TODO tags
        //TODO host-servers

        /// <summary>
        /// <para type="description">Color of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
    }
}