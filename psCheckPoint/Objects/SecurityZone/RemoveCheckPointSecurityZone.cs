using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="delete-security-zone">Remove-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointSecurityZone")]
    public class RemoveCheckPointSecurityZone : RemoveCheckPointObject
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
        protected override Task Remove(string value) => Session.DeleteSecurityZone(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}