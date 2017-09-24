using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="show-service-group">Get-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroup = Get-CheckPointGroup -Session $Session -Name Test1</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceGroup")]
    [OutputType(typeof(CheckPointServiceGroup))]
    public class GetCheckPointServiceGroup : GetCheckPointObject<CheckPointServiceGroup>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-service-group"; } }
    }
}