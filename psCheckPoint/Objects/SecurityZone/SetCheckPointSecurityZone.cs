﻿using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="set-security-zone">Set-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Edit existing Security Zone using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointSecurityZone -Name MyZone -Comments "This is my zone"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointSecurityZone")]
    [OutputType(typeof(Koopman.CheckPoint.SecurityZone))]
    public class SetCheckPointSecurityZone : SetCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Security Zone object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject SecurityZone { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(SecurityZone);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task Set(string value)
        {
            var o = Session.UpdateSecurityZone(value);
            UpdateProperties(o);
            await o.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
            WriteObject(o);
        }

        #endregion Methods
    }
}