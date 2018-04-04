using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="delete-group">Remove-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointGroup -Name Test1 -Verbose
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
        protected override void Remove(string value)
        {
            Session.DeleteGroup(value, Ignore);
        }

        #endregion Methods
    }
}