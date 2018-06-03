using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="delete-group-with-exclusion">Remove-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Delete existing Group with Exclusion using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointGroupWithExclusion -Name MyGroupWithExclusion
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointGroupWithExclusion")]
    public class RemoveCheckPointGroupWithExclusion : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject GroupWithExclusion { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(GroupWithExclusion);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteGroupWithExclusion(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}