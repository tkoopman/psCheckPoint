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
        /// <para type="description">Summary of Where Used Directly results</para>
        /// </summary>
        [JsonProperty(PropertyName = "used-directly", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedDirectly { get; set; }

        /// <summary>
        /// <para type="description">Summary of Where Used Indirectly results</para>
        /// </summary>
        [JsonProperty(PropertyName = "used-indirectly", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedResults UsedIndirectly { get; set; }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used results</para>
    /// </summary>
    public class CheckPointWhereUsedResults
    {
        /// <summary>
        /// <para type="description">Total results found</para>
        /// </summary>
        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }

        /// <summary>
        /// <para type="description">Summary of Check Point Objects found</para>
        /// </summary>
        [JsonProperty(PropertyName = "objects", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Objects { get; set; }

        /// <summary>
        /// <para type="description">Summary of Access Control Rules found</para>
        /// </summary>
        [JsonProperty(PropertyName = "access-control-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedRule[] AccessControlRules { get; set; }

        /// <summary>
        /// <para type="description">Summary of NATs found</para>
        /// </summary>
        [JsonProperty(PropertyName = "nat-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedNAT[] NATRules { get; set; }

        /// <summary>
        /// <para type="description">Summary of Threat Prevention Rules found</para>
        /// </summary>
        [JsonProperty(PropertyName = "threat-prevention-rules", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointWhereUsedThreatRule[] ThreatPreventionRules { get; set; }
    }

    /// <summary>
    /// <para type="description">Summary of Where Used Rule results</para>
    /// </summary>
    public class CheckPointWhereUsedRule : ICheckPointObjectSummary
    {
        /// <summary>
        /// <para type="description">Summary of Access Control Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// <para type="description">Summary of Layer rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; set; }

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
                return results.First();
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
        /// <para type="description">Summary of NAT Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// <para type="description">Summary of Package NAT rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "package", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; set; }

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
        /// <para type="description">Summary of Threat Rule found</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Rule { get; set; }

        /// <summary>
        /// <para type="description">Columns where object is used in rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-columns", NullValueHandling = NullValueHandling.Ignore)]
        public string[] RuleColumns { get; set; }

        /// <summary>
        /// <para type="description">Rule position</para>
        /// </summary>
        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// <para type="description">Summary of Layer rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Layer { get; set; }

        /// <summary>
        /// <para type="description">Layer position</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer-position", NullValueHandling = NullValueHandling.Ignore)]
        public string LayerPosition { get; set; }

        /// <summary>
        /// <para type="description">Summary of Package NAT rule exists in.</para>
        /// </summary>
        [JsonProperty(PropertyName = "package", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Package { get; set; }

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