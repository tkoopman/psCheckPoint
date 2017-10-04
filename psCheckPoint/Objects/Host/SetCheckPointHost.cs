using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="set-host">Set-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Set-CheckPointHost -Name Test1 -NewName Test2 -Tags TestTag</code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointHost")]
    [OutputType(typeof(CheckPointHost))]
    public class SetCheckPointHost : SetCheckPointObject<CheckPointHost>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "set-host"; } }

        /// <summary>
        /// <para type="description">IPv4 or IPv6 address. If both addresses are required use ipv4-address and ipv6-address fields explicitly.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string IPv6Address { get; set; }

        //TODO interfaces
        //TODO nat-settings
        //TODO host-servers

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