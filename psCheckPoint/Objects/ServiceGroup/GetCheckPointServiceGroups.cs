using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceGroups")]
    [OutputType(typeof(CheckPointObjects<CheckPointServiceGroup>))]
    public class GetCheckPointServiceGroups : GetCheckPointObjects<CheckPointServiceGroup>
    {
        public override string Command { get { return "show-service-groups"; } }
    }
}