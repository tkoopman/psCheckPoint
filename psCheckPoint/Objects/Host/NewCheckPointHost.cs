using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpHost = New-CheckPointHost -Session $Session -Name Test1 -ipAddress 1.2.3.4</code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointHost")]
    [OutputType(typeof(CheckPointHost))]
    public class NewCheckPointHost : NewCheckPointObject<CheckPointHost>
    {
        public override string Command { get { return "add-host"; } }

        /// <summary>
        /// <para type="description">IPv4 or IPv6 address. If both addresses are required use ipv4-address and ipv6-address fields explicitly.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string ipAddress { get; set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string ipv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string ipv6Address { get; set; }

        //TODO interfaces
        //TODO nat-settings
        //TODO host-servers

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