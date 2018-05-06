using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="show-multicast-address-ranges">Get-CheckPointMulticastAddressRanges</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointMulticastAddressRanges")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.MulticastAddressRange>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.MulticastAddressRange[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointMulticastAddressRanges : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindMulticastAddressRanges(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllMulticastAddressRanges(
                            limit: Limit,
                            detailLevel: DetailsLevel,
                            cancellationToken: CancelProcessToken), false);
            }
        }

        #endregion Methods
    }
}