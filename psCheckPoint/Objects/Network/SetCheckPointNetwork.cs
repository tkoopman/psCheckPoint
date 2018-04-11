using Koopman.CheckPoint;
using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;
using System.Net;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="set-network">Set-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointNetwork -Name Test1 -NewName Test2 -Tags TestTag
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointNetwork")]
    [OutputType(typeof(Koopman.CheckPoint.Network))]
    public class SetCheckPointNetwork : SetCheckPointCmdlet
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Allow broadcast address inclusion.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateSet("disallow", "allow", IgnoreCase = true)]
        public string Broadcast { get; set; }

        /// <summary>
        /// <para type="description">Action to take with groups.</para>
        /// </summary>
        [Parameter]
        public MembershipActions GroupAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// <para type="description">
        /// Groups listed will be either Added, Removed or replace the current list of group
        /// membership based on GroupAction parameter.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get => _groups; set => _groups = CreateArray(value); }

        /// <summary>
        /// <para type="description">
        /// IPv4 or IPv6 network mask length. If both masks are required use mask-length4 and
        /// mask-length6 fields explicitly. Instead of IPv4 mask length it is possible to specify
        /// IPv4 mask itself in subnet-mask field.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network mask length.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int MaskLength6 { get; set; }

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject Network { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">
        /// IPv4 or IPv6 network address. If both addresses are required use subnet4 and subnet6
        /// fields explicitly.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet6 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress SubnetMask { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(Network);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var o = Session.UpdateNetwork(value);
            UpdateProperties(o);
            o.AcceptChanges(Ignore);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            var o = (Koopman.CheckPoint.Network)obj;
            switch (name)
            {
                case nameof(Broadcast):
                    o.BroadcastInclusion = !(Broadcast == "disallow");
                    return true;

                case nameof(GroupAction):
                    if (GroupAction == MembershipActions.Replace && Groups == null)
                        o.Groups.Clear();
                    return true;

                case nameof(Groups):
                    o.Groups.Add(GroupAction, Groups);
                    return true;

                case nameof(MaskLength):
                    if (Subnet == null)
                        throw new PSArgumentNullException(nameof(Subnet), "Must specify Subnet to use MaskLength.");
                    if (Subnet.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        o.MaskLength6 = MaskLength;
                    else
                        o.MaskLength4 = MaskLength;
                    return true;

                case nameof(Subnet):
                    if (Subnet.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        o.Subnet6 = Subnet;
                    else
                        o.Subnet4 = Subnet;
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}