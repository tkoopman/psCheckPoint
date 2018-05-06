using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-groups">Get-CheckPointGroups</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroups
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroups")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.Group>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.Group[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointGroups : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindGroups(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllGroups(
                            limit: Limit,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
        }

        #endregion Methods
    }
}