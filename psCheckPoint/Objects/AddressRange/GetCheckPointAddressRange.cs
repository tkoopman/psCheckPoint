using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="show-address-range">Get-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAddressRange")]
    [OutputType(typeof(Koopman.CheckPoint.AddressRange))]
    public class GetCheckPointAddressRange : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindAddressRange(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}