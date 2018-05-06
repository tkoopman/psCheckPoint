using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="add-network">New-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// New-CheckPointNetwork -Name Test1 ...
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointNetwork")]
    [OutputType(typeof(Koopman.CheckPoint.Network))]
    public class NewCheckPointNetwork : NewCheckPointObject
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
        /// <para type="description">Collection of group identifiers.</para>
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
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateRange(0, 128)]
        public int MaskLength { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateRange(0, 32)]
        public int MaskLength4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network mask length.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4 and IPv6 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateRange(0, 128)]
        public int MaskLength6 { get; set; }

        /// <summary>
        /// <para type="description">
        /// IPv4 or IPv6 network address. If both addresses are required use subnet4 and subnet6
        /// fields explicitly.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4 and IPv6 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet4 { get; set; }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4 and IPv6 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public IPAddress Subnet6 { get; set; }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4 and IPv6 with subnet mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public IPAddress SubnetMask { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName.StartsWith("IPv4 or IPv6"))
            {
                if (Subnet.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    Subnet6 = Subnet;
                    MaskLength6 = MaskLength;
                }
                else
                {
                    Subnet4 = Subnet;
                    MaskLength4 = MaskLength;
                }
            }
            var network = new Koopman.CheckPoint.Network(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments,
                BroadcastInclusion = !(Broadcast == "disallow")
            };

            if (Subnet4 != null)
            {
                network.Subnet4 = Subnet4;
                if (ParameterSetName.EndsWith("subnet mask"))
                    network.SubnetMask = SubnetMask;
                else
                    network.MaskLength4 = MaskLength4;
            }

            if (Subnet6 != null)
            {
                network.Subnet6 = Subnet6;
                network.MaskLength6 = MaskLength6;
            }

            foreach (string g in Groups ?? Enumerable.Empty<string>())
                network.Groups.Add(g);
            foreach (string t in Tags ?? Enumerable.Empty<string>())
                network.Tags.Add(t);

            await network.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);

            WriteObject(network);
        }

        #endregion Methods
    }
}