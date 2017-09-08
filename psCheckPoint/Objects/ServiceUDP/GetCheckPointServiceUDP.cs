using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
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
        public override string Command { get { return "show-service-udp"; } }
    }
}