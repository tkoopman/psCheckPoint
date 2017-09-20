using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-group">Get-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroup = Get-CheckPointGroup -Session $Session -Name Test1</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroup")]
    [OutputType(typeof(CheckPointGroup))]
    public class GetCheckPointGroup : GetCheckPointObject<CheckPointGroup>
    {
        public override string Command { get { return "show-group"; } }
    }
}