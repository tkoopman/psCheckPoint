using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="show-services-udp">Get-CheckPointServicesUDP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServicesUDP")]
    [OutputType(typeof(CheckPointObjects<CheckPointService>))]
    public class GetCheckPointServicesUDP : GetCheckPointObjectsBase<CheckPointObjects<CheckPointService>>
    {
        public override string Command { get { return "show-services-udp"; } }
    }
}