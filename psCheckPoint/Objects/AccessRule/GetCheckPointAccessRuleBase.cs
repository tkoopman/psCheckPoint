using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.AccessRule
{
    /// <api cmd="show-access-rulebase">Get-CheckPointAccessRuleBase</api>
    /// <summary>
    /// <para type="synopsis">Shows the entire Access Rules layer.</para>
    /// <para type="description">Shows the entire Access Rules layer. This layer is divided into sections. An Access Rule may be within a section, or independent of a section (in which case it is said to be under the "global" section). The reply features a list of objects. Each object may be a section of the layer, with all its rules in, or a rule itself, for the case of rules which are under the global section. An optional "filter" field may be added in order to filter out only those rules that match a search criteria.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessRuleBase")]
    [OutputType(typeof(CheckPointAccessRule))]
    public class GetCheckPointAccessRuleBase : CheckPointCmdlet<CheckPointAccessRuleBase>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-access-rulebase"; } }

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
        /// <para type="description">Search expression to filter the rulebase. The provided text should be exactly the same as it would be given in Smart Console. The logical operators in the expression ('AND', 'OR') should be provided in capital letters.</para>
        /// </summary>
        [JsonProperty(PropertyName = "filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter()]
        public string Filter { get; set; }

        //TODO hit-settings
        //TODO show-hits

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "use-object-dictionary", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue(true)]
        protected bool UseObjectDictionary { get; set; } = false;

        protected override void WriteRecordResponse(CheckPointAccessRuleBase result)
        {
            foreach (CheckPointAccessRuleSummary rule in result.RuleBase)
            {
                rule.Layer = result.UID;
            }

            base.WriteRecordResponse(result);
        }
    }
}