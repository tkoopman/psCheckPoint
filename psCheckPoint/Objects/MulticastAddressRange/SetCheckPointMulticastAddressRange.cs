using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="set-multicast-address-range">Set-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointMulticastAddressRange")]
    [OutputType(typeof(CheckPointMulticastAddressRange))]
    public class SetCheckPointMulticastAddressRange : SetCheckPointObject<CheckPointMulticastAddressRange>
    {
        public override string Command { get { return "set-multicast-address-range"; } }

        /// <summary>
        /// <para type="description">First IP address in the range. If both IPv4 and IPv6 address ranges are required, use the ipv4-address-first and the ipv6-address-first fields instead.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address-first", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPAddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IP address in the range. If both IPv4 and IPv6 address ranges are required, use the ipv4-address-first and the ipv6-address-first fields instead.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address-last", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPAddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-first", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv4AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address-last", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv4AddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-first", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv6AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address-last", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv6AddressLast { get; set; }

        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        private dynamic _groups;

        /// <summary>
        /// <para type="description">Action to take with groups.</para>
        /// </summary>
        [Parameter]
        public MembershipActions GroupAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// <para type="description">Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            _groups = ProcessGroupAction(GroupAction, Groups);
        }
    }
}