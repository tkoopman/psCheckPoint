using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="show-address-range">Get-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAddressRange")]
    [OutputType(typeof(CheckPointAddressRange))]
    public class GetCheckPointAddressRange : GetCheckPointObject<CheckPointAddressRange>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-address-range"; } }
    }
}