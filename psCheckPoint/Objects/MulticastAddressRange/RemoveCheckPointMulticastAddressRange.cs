﻿using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="delete-multicast-address-range">Remove-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Delete existing Multicast Address Range using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointMulticastAddressRange -Name MyMulticastAR
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointMulticastAddressRange")]
    public class RemoveCheckPointMulticastAddressRange : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Multicast Address Range object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject MulticastAddressRange { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(MulticastAddressRange);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteMulticastAddressRange(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}