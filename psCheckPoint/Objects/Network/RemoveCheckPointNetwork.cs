using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="delete-network">Remove-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointNetwork -Name Test1 -Verbose
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointNetwork")]
    public class RemoveCheckPointNetwork : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject Network { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(Network);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Remove(string value) => Session.DeleteNetwork(value, Ignore);

        #endregion Methods
    }
}