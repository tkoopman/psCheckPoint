using System.Management.Automation;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="show-multicast-address-range">Get-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointMulticastAddressRange")]
    [OutputType(typeof(CheckPointMulticastAddressRange))]
    public class GetCheckPointMulticastAddressRange : GetCheckPointObject<CheckPointMulticastAddressRange>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-multicast-address-range"; } }
    }
}