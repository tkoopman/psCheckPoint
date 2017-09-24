using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="set-network">Set-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpNetwork = Set-CheckPointNetwork -Session $Session -Name Test1 -NewName Test2 -Tags TestTag</code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointNetwork")]
    [OutputType(typeof(CheckPointNetwork))]
    public class SetCheckPointNetwork : SetCheckPointObject<CheckPointNetwork>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "set-network"; } }

        /// <summary>
        /// <para type="description">IPv4 or IPv6 network address. If both addresses are required use subnet4 and subnet6 fields explicitly.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Subnet { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet4", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Subnet4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet6", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Subnet6 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 or IPv6 network mask length. If both masks are required use mask-length4 and mask-length6 fields explicitly. Instead of IPv4 mask length it is possible to specify IPv4 mask itself in subnet-mask field.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length4", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length6", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength6 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet-mask", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string SubnetMask { get; set; }

        //TODO nat-settings

        /// <summary>
        /// <para type="description">Allow broadcast address inclusion.</para>
        /// </summary>
        [JsonProperty(PropertyName = "broadcast", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateSet("disallow", "allow", IgnoreCase = true)]
        public string Broadcast { get; set; }

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