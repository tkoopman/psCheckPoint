using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="delete-host">Remove-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Delete existing host using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointHost -Name MyHost
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointHost")]
    public class RemoveCheckPointHost : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Host object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public new PSObject Host { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(Host);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteHost(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}