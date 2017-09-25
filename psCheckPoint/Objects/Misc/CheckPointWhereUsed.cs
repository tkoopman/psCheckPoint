using Newtonsoft.Json;
using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Session;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <summary>
    /// <para type="description">Summary of Where Used results</para>
    /// </summary>
    public class CheckPointWhereUsed
    {
        /// <summary>
        /// JSON Constructor for Where Used Results Summary
        /// </summary>
        [JsonConstructor]
        private CheckPointWhereUsed(CheckPointWhereUsedResults usedDirectly, CheckPointWhereUsedResults usedIndirectly)
        {
            UsedDirectly = usedDirectly;
            UsedIndirectly = usedIndirectly;
        }

        /// <summary>
        /// <para type="description">Summary of Where Used Directly results</para>
        /// </summary>
        [JsonProperty(PropertyName = "used-directly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedDirectly { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Where Used Indirectly results</para>
        /// </summary>
        [JsonProperty(PropertyName = "used-indirectly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedIndirectly { get; private set; }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used Result Details</para>
    /// </summary>
    public class CheckPointWhereUsedResults
    {
        /// <summary>
        /// JSON Constructor for Where Used Result Details
        /// </summary>
        [JsonConstructor]
        private CheckPointWhereUsedResults(int total, CheckPointObject[] objects, CheckPointWhereUsedRule[] accessControlRules, CheckPointWhereUsedNAT[] nATRules, CheckPointWhereUsedThreatRule[] threatPreventionRules)
        {
            Total = total;
            Objects = objects;
            AccessControlRules = accessControlRules;
            NATRules = nATRules;
            ThreatPreventionRules = threatPreventionRules;
        }

        /// <summary>
        /// <para type="description">Total results found</para>
        /// </summary>
        [JsonProperty(PropertyName = "total", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Check Point Objects found</para>
        /// </summary>
        [JsonProperty(PropertyName = "objects", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Objects { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Access Control Rules found</para>
        /// </summary>
        [JsonProperty(PropertyName = "access-control-rules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedRule[] AccessControlRules { get; private set; }

        /// <summary>
        /// <para type="description">Summary of NATs found</para>
        /// </summary>
        [JsonProperty(PropertyName = "nat-rules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedNAT[] NATRules { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Threat Prevention Rules found</para>
        /// </summary>
        [JsonProperty(PropertyName = "threat-prevention-rules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedThreatRule[] ThreatPreventionRules { get; private set; }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used Rule results</para>
    /// </summary>
    public class CheckPointWhereUsedRule : ICheckPointObjectSummary
    {
        /// <summary>
        /// JSON Constructor for Where Used Rule Results
        /// </summary>
        [JsonConstructor]
        private CheckPointWhereUsedRule(CheckPointObject rule, string[] ruleColumns, int position, CheckPointObject layer)
        {
            Rule = rule;
            RuleColumns = ruleColumns;
            Position = position;
            Layer = layer;
        }

        /// <summary>
        /// <para type="description">Summary of Access Control Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; private set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; private set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Position { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Layer rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; private set; }

        /// <summary>
        /// <para type="description">Summary object type</para>
        /// </summary>
        public string Type { get { return "access-rule"; } }

        /// <summary>
        /// <para type="description">Return full Access Rule object</para>
        /// </summary>
        public CheckPointObject ToFullObj(CheckPointSession Session)
        {
            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo("Get-CheckPointAccessRule", typeof(GetCheckPointAccessRule)));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", this.Rule.UID);
                PSI.AddParameter("Layer", this.Layer.UID);

                Collection<CheckPointAccessRule> results = PSI.Invoke<CheckPointAccessRule>();

                // Because the standard API call show-access-rule does not return rule number
                // Keep rule number from summary if it exists from API call where-used.
                CheckPointObject obj = results.First();
                if (obj is CheckPointAccessRuleSummary)
                {
                    CheckPointAccessRuleSummary rule = (obj as CheckPointAccessRuleSummary);
                    if (rule.RuleNumber == 0)
                    {
                        rule.RuleNumber = this.Position;
                    }
                }

                return obj;
            }
        }

        /// <summary>
        /// <para type="description">Return full Access Rule object</para>
        /// </summary>
        public T ToFullObj<T>(CheckPointSession Session) where T : CheckPointObject
        {
            CheckPointObject r = ToFullObj(Session);
            if (r is T)
            {
                return (r as T);
            }
            else
            {
                throw new InvalidCastException($"{r.GetType()} cannot be cast to {typeof(T).ToString()}");
            }
        }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used NAT results</para>
    /// </summary>
    public class CheckPointWhereUsedNAT : ICheckPointObjectSummary
    {
        /// <summary>
        /// JSON Constructor for Where Used NAT Results
        /// </summary>
        [JsonConstructor]
        private CheckPointWhereUsedNAT(CheckPointObject rule, string[] ruleColumns, string position, CheckPointObject package)
        {
            Rule = rule;
            RuleColumns = ruleColumns;
            Position = position;
            Package = package;
        }

        /// <summary>
        /// <para type="description">Summary of NAT Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; private set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; private set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Package NAT rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "package", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; private set; }

        /// <summary>
        /// <para type="description">Summary object type</para>
        /// </summary>
        public string Type => throw new System.NotImplementedException();

        /// <summary>
        /// <para type="description">Return full NAT Rule object</para>
        /// </summary>
        public CheckPointObject ToFullObj(CheckPointSession Session)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <para type="description">Return full NAT Rule object</para>
        /// </summary>
        public T ToFullObj<T>(CheckPointSession Session) where T : CheckPointObject
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used Threat Rule results</para>
    /// </summary>
    public class CheckPointWhereUsedThreatRule : ICheckPointObjectSummary
    {
        /// <summary>
        /// JSON Constructor for Where Used Threat Rule Results
        /// </summary>
        [JsonConstructor]
        private CheckPointWhereUsedThreatRule(CheckPointObject rule, string[] ruleColumns, string position, CheckPointObject layer, string layerPosition, CheckPointObject package)
        {
            Rule = rule;
            RuleColumns = ruleColumns;
            Position = position;
            Layer = layer;
            LayerPosition = layerPosition;
            Package = package;
        }

        /// <summary>
        /// <para type="description">Summary of Threat Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; private set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; private set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Layer rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; private set; }

        /// <summary>
        /// <para type="description">Layer position</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer-position", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string LayerPosition { get; private set; }

        /// <summary>
        /// <para type="description">Summary of Package NAT rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "package", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; private set; }

        /// <summary>
        /// <para type="description">Summary object type</para>
        /// </summary>
        public string Type => throw new System.NotImplementedException();

        /// <summary>
        /// <para type="description">Return full Threat Rule object</para>
        /// </summary>
        public CheckPointObject ToFullObj(CheckPointSession Session)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <para type="description">Return full Threat Rule object</para>
        /// </summary>
        public T ToFullObj<T>(CheckPointSession Session) where T : CheckPointObject
        {
            throw new System.NotImplementedException();
        }
    }
}