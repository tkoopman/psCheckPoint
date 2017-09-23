using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="show-security-zones">Get-CheckPointSecurityZones</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSecurityZones")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointSecurityZones : GetCheckPointObjects
    {
        public override string Command { get { return "show-security-zones"; } }
    }
}