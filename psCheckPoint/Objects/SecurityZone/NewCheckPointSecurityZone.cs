using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
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
        public override string Command { get { return "add-security-zone"; } }
    }
}