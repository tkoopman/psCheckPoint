using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="delete-service-tcp">Remove-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Delete existing TCP object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointServiceTCP -Name MyTCP
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceTCP")]
    public class RemoveCheckPointServiceTCP : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceTCP { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceTCP);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteServiceTCP(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}