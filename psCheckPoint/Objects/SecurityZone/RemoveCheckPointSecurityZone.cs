using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointSecurityZone")]
    public class RemoveCheckPointSecurityZone : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-security-zone"; } }
    }
}