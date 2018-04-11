using System.Management.Automation;

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
        protected override void ProcessRecord() => WriteObject(Session.FindMulticastAddressRange(Value, DetailsLevel));

        #endregion Methods
    }
}