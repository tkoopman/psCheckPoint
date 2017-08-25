using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroups = Get-CheckPointGroups -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroups")]
    [OutputType(typeof(CheckPointObjects<CheckPointGroup>))]
    public class GetCheckPointGroups : GetCheckPointObjects<CheckPointGroup>
    {
        public override string Command { get { return "show-groups"; } }
    }
}