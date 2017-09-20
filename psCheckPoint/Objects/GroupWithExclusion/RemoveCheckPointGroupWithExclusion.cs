using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="delete-group-with-exclusion">Remove-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointGroupWithExclusion")]
    public class RemoveCheckPointGroupWithExclusion : RemoveCheckPointObject<CheckPointMessage>
    {
        public override string Command { get { return "delete-group-with-exclusion"; } }
    }
}