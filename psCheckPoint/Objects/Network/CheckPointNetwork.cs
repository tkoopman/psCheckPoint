using Newtonsoft.Json;

namespace psCheckPoint.Objects.Network
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPointHost, Get-CheckPointHost & Get-CheckPointHosts</para>
    /// <para type="description">Host object details.</para>
    /// </summary>
    public class CheckPointNetwork : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public CheckPointObject[] Groups { get; set; }

        //TODO host-servers
        //TODO nat-settings

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet4")]
        public string Subnet4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet6")]
        public string Subnet6 { get; set; }

        /// <summary>
        /// <para type="description">Allow broadcast address inclusion.</para>
        /// </summary>
        [JsonProperty(PropertyName = "broadcast")]
        public string Broadcast { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length4")]
        public int MaskLength4 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length6")]
        public int MaskLength6 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet-mask")]
        public string SubnetMask { get; set; }
    }
}