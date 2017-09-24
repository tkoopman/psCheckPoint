using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="set-security-zone">Set-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointSecurityZone")]
    [OutputType(typeof(CheckPointSecurityZone))]
    public class SetCheckPointSecurityZone : SetCheckPointObject<CheckPointSecurityZone>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "set-security-zone"; } }
    }
}