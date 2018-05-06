using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="show-multicast-address-range">Get-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointMulticastAddressRange")]
    [OutputType(typeof(Koopman.CheckPoint.MulticastAddressRange))]
    public class GetCheckPointMulticastAddressRange : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindMulticastAddressRange(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}