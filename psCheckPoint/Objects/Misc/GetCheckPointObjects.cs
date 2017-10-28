using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-objects">Get-CheckPointObjects</api>
    /// <api cmd="show-unused-objects">Get-CheckPointObjects</api>
    /// <summary>
    /// <para type="synopsis">Find objects by Filter.</para>
    /// <para type="description">Can find many different types of objects based on a filter. Filters are same as what can be used in Smart Console</para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointObjects -Filter "O365 OR Office365"</code>
    /// </example>
    /// <example>
    /// <code>Get-CheckPointObjects -Unused</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointObjects", DefaultParameterSetName = "Limit + Filter")]
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointObjects : CheckPointCmdlet<CheckPointObjects>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return (Unused.IsPresent) ? "show-unused-objects" : "show-objects"; } }

        /// <summary>
        /// <para type="description">No more than that many results will be returned.</para>
        /// </summary>
        [JsonProperty(PropertyName = "limit", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        [ValidateRange(1, 500)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [JsonProperty(PropertyName = "offset", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ParameterSetName = "Limit + Filter")]
        [Parameter(ParameterSetName = "Limit + Unused")]
        public int Offset { get; set; } = 0;

        /// <summary>
        /// <para type="description">Get All Records</para>
        /// </summary>
        [Parameter(ParameterSetName = "All + Filter", Mandatory = true)]
        [Parameter(ParameterSetName = "All + Unused", Mandatory = true)]
        public SwitchParameter All { get; set; }

        /// <summary>
        /// <para type="description">Retrieve all unused objects.</para>
        /// </summary>
        [Parameter(ParameterSetName = "Limit + Unused", Mandatory = true)]
        [Parameter(ParameterSetName = "All + Unused", Mandatory = true)]
        public SwitchParameter Unused { get; set; }

        /// <summary>
        /// <para type="description">Search expression to filter objects by. The provided text should be exactly the same as it would be given in Smart Console. The logical operators in the expression ('AND', 'OR') should be provided in capital letters. By default, the search involves both a textual search and a IP search. To use IP search only, set the "ip-only" parameter to true.</para>
        /// </summary>
        [JsonProperty(PropertyName = "filter", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ParameterSetName = "Limit + Filter")]
        [Parameter(ParameterSetName = "All + Filter")]
        public string Filter { get; set; }

        /// <summary>
        /// <para type="description">If using "filter", use this field to search objects by their IP address only, without involving the textual search.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-only", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter(ParameterSetName = "Limit + Filter")]
        [Parameter(ParameterSetName = "All + Filter")]
        public SwitchParameter IPOnly { get; set; }

        /// <summary>
        /// <para type="description">The objects' type</para>
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("object")]
        [Parameter(ParameterSetName = "Limit + Filter")]
        [Parameter(ParameterSetName = "All + Filter")]
        [ValidateSet("object", "host", "network", "group", "address-range", "multicast-address-range", "group-with-exclusion", "simple-gateway", "security-zone", "time", "time-group", "access-role", "dynamic-object", "trusted-client", "tag", "dns-domain", "opsec-application",
            "service-tcp", "service-udp", "service-icmp", "service-icmp6", "service-sctp", "service-other", "service-group",
            IgnoreCase = false)]
        public string Type { get; set; } = "object";

        protected override void WriteRecordResponse(CheckPointObjects result)
        {
            if (ParameterSetName.StartsWith("Limit"))
            {
                base.WriteRecordResponse(result);
            }
            else
            {
                foreach (object r in result)
                {
                    WriteObject(r);
                }

                if (result.To != result.Total)
                {
                    Offset = result.To;
                    ProcessRecord();
                }
            }
        }
    }
}