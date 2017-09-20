using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="set-service-tcp">Set-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointServiceTCP")]
    [OutputType(typeof(CheckPointServiceTCP))]
    public class SetCheckPointServiceTCP : SetCheckPointService<CheckPointServiceTCP>
    {
        public override string Command { get { return "set-service-tcp"; } }
    }
}