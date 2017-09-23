using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-groups">Get-CheckPointGroups</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroups = Get-CheckPointGroups -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroups")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointGroups : GetCheckPointObjects
    {
        public override string Command { get { return "show-groups"; } }
    }
}