using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="show-address-ranges">Get-CheckPointAddressRanges</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all address ranges.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointAddressRanges
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAddressRanges")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.AddressRange>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.AddressRange[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointAddressRanges : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindAddressRanges(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllAddressRanges(
                            limit: Limit,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), true);
            }
        }

        #endregion Methods
    }
}