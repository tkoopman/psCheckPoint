using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="delete-access-layer">Remove-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointAccessLayer -Name Network
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointAccessLayer")]
    public class RemoveCheckPointAccessLayer : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID", "Layer")]
        public PSObject AccessLayer { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(AccessLayer);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Remove(string value) => Session.DeleteAccessLayer(value, Ignore);

        #endregion Methods
    }
}