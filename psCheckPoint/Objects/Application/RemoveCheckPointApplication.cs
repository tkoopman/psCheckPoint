using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="delete-application-site">Remove-CheckPointApplication</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointApplication")]
    public class RemoveCheckPointApplication : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Application Site object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ApplicationSite { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ApplicationSite);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteApplicationSite(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}