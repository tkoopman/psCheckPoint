using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServicesUDP")]
    [OutputType(typeof(CheckPointObjects<CheckPointServiceUDP>))]
    public class GetCheckPointServicesUDP : GetCheckPointObjects<CheckPointServiceUDP>
    {
        public override string Command { get { return "show-services-udp"; } }
    }
}