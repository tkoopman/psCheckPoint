using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    [Cmdlet(VerbsCommon.Get, "CheckPointObjects")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointObjects : GetCheckPointObjects<CheckPointObject>
    {
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
        [ValidateSet("object", "host", "network", "group", "address-range", "multicast-address-range", "group-with-exclusion", "simple-gateway", "security-zone", "time", "time-group", "access-role", "dynamic-object", "trusted-client", "tag", "dns-domain", "opsec-application", IgnoreCase = false)]
        public string Type { get; set; } = "object";
    }
}