using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
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
        public override string Command { get { return "show-address-range"; } }
    }
}