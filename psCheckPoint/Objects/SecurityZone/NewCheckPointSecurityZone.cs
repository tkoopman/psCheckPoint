using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="add-security-zone">New-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointSecurityZone")]
    [OutputType(typeof(CheckPointSecurityZone))]
    public class NewCheckPointSecurityZone : NewCheckPointObject<CheckPointSecurityZone>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "add-security-zone"; } }
    }
}