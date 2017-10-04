using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="add-network">New-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>New-CheckPointNetwork -Name Test1 ...</code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointNetwork")]
    [OutputType(typeof(CheckPointNetwork))]
    public class NewCheckPointNetwork : NewCheckPointObject<CheckPointNetwork>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "add-network"; } }

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