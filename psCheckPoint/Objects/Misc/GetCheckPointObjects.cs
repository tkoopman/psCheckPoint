using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-objects">Get-CheckPointObjects</api>
    /// <summary>
    /// <para type="synopsis">Find objects by Filter.</para>
    /// <para type="description">Can find many different types of objects based on a filter. Filters are same as what can be used in Smart Console</para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointObjects -Filter "O365 OR Office365"</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointObjects")]
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointObjects : psCheckPoint.Objects.GetCheckPointObjects
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-objects"; } }

        /// <summary>
        /// <para type="description">Search expression to filter objects by. The provided text should be exactly the same as it would be given in Smart Console. The logical operators in the expression ('AND', 'OR') should be provided in capital letters. By default, the search involves both a textual search and a IP search. To use IP search only, set the "ip-only" parameter to true.</para>
        /// </summary>
        [JsonProperty(PropertyName = "filter")]
        [Parameter]
        public string Filter { get; set; }

        /// <summary>
        /// <para type="description">If using "filter", use this field to search objects by their IP address only, without involving the textual search.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-only", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter IPOnly { get; set; }

        /// <summary>
        /// <para type="description">The objects' type</para>
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("object")]
        [Parameter]
        [ValidateSet("object", "host", "network", "group", "address-range", "multicast-address-range", "group-with-exclusion", "simple-gateway", "security-zone", "time", "time-group", "access-role", "dynamic-object", "trusted-client", "tag", "dns-domain", "opsec-application",
            "service-tcp", "service-udp", "service-icmp", "service-icmp6", "service-sctp", "service-other", "service-group",
            IgnoreCase = false)]
        public string Type { get; set; } = "object";
    }
}