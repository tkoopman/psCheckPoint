using System.Management.Automation;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="delete-application-site">Remove-CheckPointApplication</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointApplication")]
    public class RemoveCheckPointApplication : RemoveCheckPointObject
    {
        public override string Command { get { return "delete-application-site"; } }
    }
}