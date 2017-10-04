using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-group">Get-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroup = Get-CheckPointGroup -Name Test1</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroup")]
    [OutputType(typeof(CheckPointGroup))]
    public class GetCheckPointGroup : GetCheckPointObject<CheckPointGroup>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-group"; } }
    }
}