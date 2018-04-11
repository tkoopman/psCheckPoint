using System.Management.Automation;

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
        protected override void ProcessRecord() => WriteObject(Session.FindAddressRange(Value, DetailsLevel));

        #endregion Methods
    }
}