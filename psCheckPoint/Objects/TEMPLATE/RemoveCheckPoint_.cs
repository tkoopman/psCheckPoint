using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPoint_")]
    public class RemoveCheckPoint_ : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-_"; } }
    }
}