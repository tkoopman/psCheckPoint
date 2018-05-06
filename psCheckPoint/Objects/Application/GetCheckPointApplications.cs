using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="show-application-sites">Get-CheckPointApplications</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplications")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.ApplicationSite>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationSite[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointApplications : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindApplicationSites(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllApplicationSites(
                            limit: Limit,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
        }

        #endregion Methods
    }
}