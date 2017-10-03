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
    [OutputType(typeof(CheckPointServices))]
    public class GetCheckPointServicesUDP : GetCheckPointObjectsBase<CheckPointServices>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-services-udp"; } }
    }
}