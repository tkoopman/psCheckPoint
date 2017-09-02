using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(CheckPointGroupWithExclusion))]
    public class NewCheckPointGroupWithExclusion : NewCheckPointObject<CheckPointGroupWithExclusion>
    {
        public override string Command { get { return "add-group-with-exclusion"; } }

        /// <summary>
        /// <para type="description">Object to include.</para>
        /// </summary>
        [JsonProperty(PropertyName = "include", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Include { get; set; }

        /// <summary>
        /// <para type="description">Object to exclude.</para>
        /// </summary>
        [JsonProperty(PropertyName = "except", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Except { get; set; }
    }
}