using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <api cmd="delete-_">Remove-CheckPoint_</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPoint_")]
    public class RemoveCheckPoint_ : RemoveCheckPointObject
    {
        public override string Command { get { return "delete-_"; } }
    }
}