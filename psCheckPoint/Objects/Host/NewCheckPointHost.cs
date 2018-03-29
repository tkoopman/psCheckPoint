using System;
using System.Linq;
using System.Management.Automation;
using Koopman.CheckPoint;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="add-host">New-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>New-CheckPointHost -Name Test1 -ipAddress 1.2.3.4</code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointHost")]
    [OutputType(typeof(Koopman.CheckPoint.Host))]
    public class NewCheckPointHost : NewCheckPointObject
    {

        /// <summary>
        /// <para type="description">IPv4 or IPv6 address. If both addresses are required use ipv4-address and ipv6-address fields explicitly.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 address.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 & IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public string IPv6Address { get; set; }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups
        {
            get { return _groups; }
            set { _groups = CreateArray(value); }
        }

        private string[] _groups;

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (ParameterSetName.Equals("IPv4 or IPv6"))
            {
                var ip = System.Net.IPAddress.Parse(IPAddress);
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    IPv6Address = IPAddress;
                else
                    IPv4Address = IPAddress;
            }
            var host = new Koopman.CheckPoint.Host(Session)
            {
                Name = Name,
                Color = Color,
                Comments = Comments,
                IPv4Address = (String.IsNullOrWhiteSpace(IPv4Address)) ? null : System.Net.IPAddress.Parse(IPv4Address),
                IPv6Address = (String.IsNullOrWhiteSpace(IPv6Address)) ? null : System.Net.IPAddress.Parse(IPv6Address)
            };
            foreach (var g in Groups ?? Enumerable.Empty<string>())
                host.Groups.Add(g);
            foreach (var t in Tags ?? Enumerable.Empty<string>())
                host.Tags.Add(t);

            //TODO Set if exists
            host.AcceptChanges(Ignore);

            WriteObject(host);
        }
    }
}