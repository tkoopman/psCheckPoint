using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointNetwork -Session $Session -Name Test1 -Verbose</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointNetwork")]
    public class RemoveCheckPointNetwork : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-network"; } }
    }
}