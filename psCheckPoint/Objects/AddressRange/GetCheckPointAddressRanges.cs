using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAddressRanges")]
    [OutputType(typeof(CheckPointObjects<CheckPointAddressRange>))]
    public class GetCheckPointAddressRanges : GetCheckPointObjects<CheckPointAddressRange>
    {
        public override string Command { get { return "show-address-ranges"; } }
    }
}