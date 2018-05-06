using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="delete-service-group">Remove-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointGroup -Name Test1 -Verbose
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceGroup")]
    public class RemoveCheckPointServiceGroup : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceGroup { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceGroup);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteServiceGroup(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}