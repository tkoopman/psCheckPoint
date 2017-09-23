using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="show-service-groups">Get-CheckPointServiceGroups</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceGroups")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointServiceGroups : GetCheckPointObjects
    {
        public override string Command { get { return "show-service-groups"; } }
    }
}