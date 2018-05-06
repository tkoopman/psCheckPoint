using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="show-application-site-category">Get-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
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