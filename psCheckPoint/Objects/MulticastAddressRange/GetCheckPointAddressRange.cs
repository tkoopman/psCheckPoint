using System.Management.Automation;

namespace psCheckPoint.Objects.MulticastAddressRange
{
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
        public override string Command { get { return "show-multicast-address-range"; } }
    }
}