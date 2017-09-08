using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceUDP")]
    public class RemoveCheckPointServiceUDP : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-service-udp"; } }
    }
}