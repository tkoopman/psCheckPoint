using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-networks">Get-CheckPointNetworks</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointNetworks
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetworks")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.Network>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.Network[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointNetworks : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindNetworks(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllNetworks(
                            limit: Limit,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
        }

        #endregion Methods
    }
}