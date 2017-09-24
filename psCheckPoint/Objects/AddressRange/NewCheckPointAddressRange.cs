using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="add-address-range">New-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointAddressRange")]
    [OutputType(typeof(CheckPointAddressRange))]
    public class NewCheckPointAddressRange : NewCheckPointObject<CheckPointAddressRange>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "add-address-range"; } }

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

        //TODO nat-settings

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups
        {
            get { return _groups; }
            set { _groups = CreateArray(value); }
        }

        private string[] _groups;
    }
}