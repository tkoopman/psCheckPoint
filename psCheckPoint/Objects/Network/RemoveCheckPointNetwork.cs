using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="delete-network">Remove-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Delete existing Network using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointNetwork -Name MyNetwork
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
        protected override Task Remove(string value) => Session.DeleteNetwork(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}