using Newtonsoft.Json;
using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="add-service-udp">New-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointServiceUDP")]
    [OutputType(typeof(CheckPointServiceUDP))]
    public class NewCheckPointServiceUDP : NewCheckPointService<CheckPointServiceUDP>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "add-service-udp"; } }

        /// <summary>
        /// <para type="description">N/A</para>
        /// </summary>
        [JsonProperty(PropertyName = "accept-replies", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter AcceptReplies { get; set; }
    }
}