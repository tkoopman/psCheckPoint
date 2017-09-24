using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="show-service-udp">Get-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceUDP")]
    [OutputType(typeof(CheckPointServiceUDP))]
    public class GetCheckPointServiceUDP : GetCheckPointObject<CheckPointServiceUDP>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-service-udp"; } }
    }
}