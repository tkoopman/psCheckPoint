using Newtonsoft.Json;
using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="set-service-udp">Set-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointServiceUDP")]
    [OutputType(typeof(CheckPointServiceUDP))]
    public class SetCheckPointServiceUDP : SetCheckPointService<CheckPointServiceUDP>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "set-service-udp"; } }

        /// <summary>
        /// <para type="description">N/A</para>
        /// </summary>
        [JsonProperty(PropertyName = "accept-replies", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter AcceptReplies { get; set; }
    }
}