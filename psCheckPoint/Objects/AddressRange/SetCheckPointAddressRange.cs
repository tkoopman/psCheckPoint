using Koopman.CheckPoint;
using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;
using System.Net;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="set-address-range">Set-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Edit existing address range using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointAddressRange -Name Range1 -NewName RangeA -Color Red
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointAddressRange", DefaultParameterSetName = "IPv4 and IPv6")]
    [OutputType(typeof(Koopman.CheckPoint.AddressRange))]
    public class SetCheckPointAddressRange : SetCheckPointCmdlet
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Address Range object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject AddressRange { get => Object; set => Object = value; }

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
        /// First IP address in the range. If both IPv4 and IPv6 address ranges are required, use the
        /// ipv4-address-first and the ipv6-address-first fields instead.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPAddressFirst { get; set; }

        /// <summary>
        /// <para type="description">
        /// Last IP address in the range. If both IPv4 and IPv6 address ranges are required, use the
        /// ipv4-address-first and the ipv6-address-first fields instead.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPAddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv4 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv4AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv4AddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv6AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv6AddressLast { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(AddressRange);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task Set(string value)
        {
            var o = Session.UpdateAddressRange(value);
            UpdateProperties(o);
            await o.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            var o = (Koopman.CheckPoint.AddressRange)obj;
            switch (name)
            {
                case nameof(GroupAction):
                    if (GroupAction == MembershipActions.Replace && Groups == null)
                        o.Groups.Clear();
                    return true;

                case nameof(Groups):
                    o.Groups.Add(GroupAction, Groups);
                    return true;

                case nameof(IPAddressFirst):
                    if (IPAddressFirst.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        o.IPv6AddressFirst = IPAddressFirst;
                    else
                        o.IPv4AddressFirst = IPAddressFirst;
                    return true;

                case nameof(IPAddressLast):
                    if (IPAddressLast.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        o.IPv6AddressLast = IPAddressLast;
                    else
                        o.IPv4AddressLast = IPAddressLast;
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}