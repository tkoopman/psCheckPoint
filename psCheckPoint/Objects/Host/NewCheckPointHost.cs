using System.Linq;
using System.Management.Automation;
using System.Net;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="add-host">New-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// New-CheckPointHost -Name Test1 -ipAddress 1.2.3.4
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointHost")]
    [OutputType(typeof(Koopman.CheckPoint.Host))]
    public class NewCheckPointHost : NewCheckPointObject
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get => _groups; set => _groups = CreateArray(value); }

        /// <summary>
        /// <para type="description">
        /// IPv4 or IPv6 address. If both addresses are required use ipv4-address and ipv6-address
        /// fields explicitly.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public IPAddress IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public IPAddress IPv6Address { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (ParameterSetName.Equals("IPv4 or IPv6"))
            {
                if (IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    IPv6Address = IPAddress;
                else
                    IPv4Address = IPAddress;
            }
            var host = new Koopman.CheckPoint.Host(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments,
                IPv4Address = IPv4Address,
                IPv6Address = IPv6Address
            };
            foreach (string g in Groups ?? Enumerable.Empty<string>())
                host.Groups.Add(g);
            foreach (string t in Tags ?? Enumerable.Empty<string>())
                host.Tags.Add(t);

            host.AcceptChanges(Ignore);

            WriteObject(host);
        }

        #endregion Methods
    }
}