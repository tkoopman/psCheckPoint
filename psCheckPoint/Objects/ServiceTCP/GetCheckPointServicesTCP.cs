using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServicesTCP")]
    [OutputType(typeof(CheckPointObjects<CheckPointServiceTCP>))]
    public class GetCheckPointServicesTCP : GetCheckPointObjects<CheckPointServiceTCP>
    {
        public override string Command { get { return "show-services-tcp"; } }
    }
}