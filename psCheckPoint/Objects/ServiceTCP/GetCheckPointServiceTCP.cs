using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="show-service-tcp">Get-CheckPointServiceTCP</api>
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
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-service-tcp"; } }
    }
}