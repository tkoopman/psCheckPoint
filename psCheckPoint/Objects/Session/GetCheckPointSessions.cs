using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-sessions">Get-CheckPointSessions</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSessions")]
    [OutputType(typeof(CheckPointSessions))]
    public class GetCheckPointSessions : GetCheckPointObjectsBase<CheckPointSessions>
    {
        /// <summary>
        /// Default constructor the changes GetCheckPointObjects.DetailsLevel default setting
        /// </summary>
        public GetCheckPointSessions()
        {
            DetailsLevel = "full";
        }

        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
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