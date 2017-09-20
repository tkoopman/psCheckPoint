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
        public override string Command { get { return "set-security-zone"; } }
    }
}