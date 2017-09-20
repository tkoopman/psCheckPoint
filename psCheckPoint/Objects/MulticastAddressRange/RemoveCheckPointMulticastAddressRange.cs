using System.Management.Automation;

namespace psCheckPoint.Objects.MulticastAddressRange
{
    /// <api cmd="delete-multicast-address-range">Remove-CheckPointMulticastAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointMulticastAddressRange")]
    public class RemoveCheckPointMulticastAddressRange : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-multicast-address-range"; } }
    }
}