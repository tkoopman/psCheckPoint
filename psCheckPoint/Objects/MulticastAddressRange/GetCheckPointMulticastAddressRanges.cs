using System.Management.Automation;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="show-multicast-address-ranges">Get-CheckPointMulticastAddressRanges</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointMulticastAddressRanges")]
    [OutputType(typeof(CheckPointObjects<CheckPointMulticastAddressRange>))]
    public class GetCheckPointMulticastAddressRanges : GetCheckPointObjects<CheckPointMulticastAddressRange>
    {
        public override string Command { get { return "show-multicast-address-ranges"; } }
    }
}