using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointGroup -Session $Session -Name Test1 -Verbose</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceGroup")]
    public class RemoveCheckPointServiceGroup : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-service-group"; } }
    }
}