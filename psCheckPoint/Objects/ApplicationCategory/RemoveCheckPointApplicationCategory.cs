using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="delete-application-site-category">Remove-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointApplicationCategory")]
    public class RemoveCheckPointApplicationCategory : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Application Category object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ApplicationCategory { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ApplicationCategory);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task Remove(string value) => Session.DeleteApplicationCategory(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}