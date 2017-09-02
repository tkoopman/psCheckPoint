using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(CheckPointGroupWithExclusion))]
    public class SetCheckPointHostWithExclusion : SetCheckPointObject<CheckPointGroupWithExclusion>
    {
        public override string Command { get { return "set-group-with-exclusion"; } }

        /// <summary>
        /// <para type="description">Object to include.</para>
        /// </summary>
        [JsonProperty(PropertyName = "include", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Include { get; set; }

        /// <summary>
        /// <para type="description">Object to exclude.</para>
        /// </summary>
        [JsonProperty(PropertyName = "except", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Except { get; set; }
    }
}