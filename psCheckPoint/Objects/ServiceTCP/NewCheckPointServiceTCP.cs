using Newtonsoft.Json;
using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointServiceTCP")]
    [OutputType(typeof(CheckPointServiceTCP))]
    public class NewCheckPointServiceTCP : NewCheckPointService<CheckPointServiceTCP>
    {
        public override string Command { get { return "add-service-tcp"; } }
    }
}