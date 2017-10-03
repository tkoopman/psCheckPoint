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
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointSecurityZones : GetCheckPointObjects
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-security-zones"; } }
    }
}