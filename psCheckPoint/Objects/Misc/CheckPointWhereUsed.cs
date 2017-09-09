using Newtonsoft.Json;

namespace psCheckPoint.Objects.Misc
{
    public class CheckPointWhereUsed
    {
        [JsonProperty(PropertyName = "used-directly", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedDirectly { get; set; }

        [JsonProperty(PropertyName = "used-indirectly", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedIndirectly { get; set; }
    }

    public class CheckPointWhereUsedResults
    {
        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "objects", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Objects { get; set; }

        [JsonProperty(PropertyName = "access-control-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedRule[] AccessControlRules { get; set; }

        [JsonProperty(PropertyName = "nat-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedNAT[] NATRules { get; set; }

        [JsonProperty(PropertyName = "threat-prevention-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedThreatRule[] ThreatPreventionRules { get; set; }
    }

    public class CheckPointWhereUsedRule
    {
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "layer", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; set; }
    }

    public class CheckPointWhereUsedNAT
    {
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "package", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; set; }
    }

    public class CheckPointWhereUsedThreatRule
    {
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "layer", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; set; }

        [JsonProperty(PropertyName = "layer-position", NullValueHandling = NullValueHandling.Ignore)]
        public string LayerPosition { get; set; }

        [JsonProperty(PropertyName = "package", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; set; }
    }
}