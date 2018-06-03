using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="show-application-site-category">Get-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Application Category using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointApplicationCategory -Name "Low Risk"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplicationCategory")]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationCategory))]
    public class GetCheckPointApplicationCategory : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindApplicationCategory(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}