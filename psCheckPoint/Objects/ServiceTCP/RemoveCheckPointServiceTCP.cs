using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="delete-service-tcp">Remove-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceTCP")]
    public class RemoveCheckPointServiceTCP : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-service-tcp"; } }
    }
}