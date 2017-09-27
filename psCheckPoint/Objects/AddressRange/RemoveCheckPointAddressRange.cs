using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="delete-address-range">Remove-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointAddressRange")]
    public class RemoveCheckPointAddressRange : RemoveCheckPointObject
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "delete-address-range"; } }
    }
}