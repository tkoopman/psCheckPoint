using System.Management.Automation;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="delete-host">Remove-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointHost -Session $Session -Name Test1 -Verbose</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointHost")]
    public class RemoveCheckPointHost : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-host"; } }
    }
}