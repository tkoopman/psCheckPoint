using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceTCP")]
    [OutputType(typeof(CheckPointServiceTCP))]
    public class GetCheckPointServiceTCP : GetCheckPointObject<CheckPointServiceTCP>
    {
        public override string Command { get { return "show-service-tcp"; } }
    }
}