using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="delete-group">Remove-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Delete existing group using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointGroup -Name MyGroup
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointGroup")]
    public class RemoveCheckPointGroup : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Group object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject Group { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(Group);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteGroup(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}