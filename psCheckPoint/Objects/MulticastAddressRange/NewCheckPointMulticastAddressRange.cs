﻿using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="add-multicast-address-range">New-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Create new Multicast Address Range.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// New-CheckPointMulticastAddressRange -Name MyMulticastAR -IPAddress 224.5.6.7
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointMulticastAddressRange")]
    [OutputType(typeof(Koopman.CheckPoint.MulticastAddressRange))]
    public class NewCheckPointMulticastAddressRange : NewCheckPointObject
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
        /// First IP address in the range. If both IPv4 and IPv6 address ranges are required, use the
        /// ipv4-address-first and the ipv6-address-first fields instead.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 or IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("IPAddress")]
        [AllowNull]
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
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("IPv4Address")]
        [AllowNull]
        public IPAddress IPv4AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv4 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv4", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv4AddressLast { get; set; }

        /// <summary>
        /// <para type="description">First IPv6 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("IPv6Address")]
        [AllowNull]
        public IPAddress IPv6AddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IPv6 address in the range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "IPv4 and IPv6", ValueFromPipelineByPropertyName = true)]
        [Parameter(ParameterSetName = "IPv6", ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv6AddressLast { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName.StartsWith("IPv4 or IPv6"))
            {
                if (IPAddressFirst.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    IPv6AddressFirst = IPAddressFirst;
                    IPv6AddressLast = IPAddressLast;
                }
                else
                {
                    IPv4AddressFirst = IPAddressFirst;
                    IPv4AddressLast = IPAddressLast;
                }
            }

            // Allow specifying a Single Address Multicast Address Range by only entering a single address
            if (IPv4AddressFirst != null && IPv4AddressLast == null) { IPv4AddressLast = IPv4AddressFirst; }
            if (IPv6AddressFirst != null && IPv6AddressLast == null) { IPv6AddressLast = IPv6AddressFirst; }

            var addressRange = new Koopman.CheckPoint.MulticastAddressRange(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments,
                IPv4AddressFirst = IPv4AddressFirst,
                IPv4AddressLast = IPv4AddressLast,
                IPv6AddressFirst = IPv6AddressFirst,
                IPv6AddressLast = IPv6AddressLast
            };

            foreach (string g in Groups ?? Enumerable.Empty<string>())
                addressRange.Groups.Add(g);
            foreach (string t in Tags ?? Enumerable.Empty<string>())
                addressRange.Tags.Add(t);

            await addressRange.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);

            WriteObject(addressRange);
        }

        #endregion Methods
    }
}