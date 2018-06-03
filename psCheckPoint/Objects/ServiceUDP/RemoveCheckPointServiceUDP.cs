﻿using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="delete-service-udp">Remove-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Delete existing UDP object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointServiceUDP -Name MyUDP
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceUDP")]
    public class RemoveCheckPointServiceUDP : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceUDP { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceUDP);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteServiceUDP(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}