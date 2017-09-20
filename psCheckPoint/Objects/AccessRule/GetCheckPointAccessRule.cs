using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.AccessRule
{
    /// <api cmd="show-access-rule">Get-CheckPointAccessRule</api>
    /// <summary>
    ///
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessRule")]
    [OutputType(typeof(CheckPointAccessRule))]
    public class GetCheckPointAccessRule : CheckPointCmdlet<CheckPointAccessRule>
    {
        public override string Command { get { return "show-access-rule"; } }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By UID", ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Object name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Position = 2, Mandatory = true, ParameterSetName = "By Name", ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Rule number.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By Rule Number", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public int RuleNumber { get; set; }

        /// <summary>
        /// <para type="description">Layer that the rule belongs to identified by the name or UID.</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Layer { get; set; }

        //TODO hit-seetings
        //TODO show-hits

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";
    }
}