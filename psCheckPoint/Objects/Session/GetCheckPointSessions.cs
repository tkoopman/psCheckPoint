using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSessions")]
    [OutputType(typeof(CheckPointObjects<CheckPointSession>))]
    public class GetCheckPointSessions : GetCheckPointObjects<CheckPointSession>
    {
        public GetCheckPointSessions()
        {
            DetailsLevel = "full";
        }

        public override string Command { get { return "show-sessions"; } }

        /// <summary>
        /// <para type="description">Show a list of published sessions.</para>
        /// </summary>
        [JsonProperty(PropertyName = "view-published-sessions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter ViewPublishedSessions { get; set; }
    }
}