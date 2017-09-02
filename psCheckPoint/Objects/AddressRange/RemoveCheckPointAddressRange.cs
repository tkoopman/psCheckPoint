using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointAddressRange")]
    public class RemoveCheckPointAddressRange : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-address-range"; } }
    }
}