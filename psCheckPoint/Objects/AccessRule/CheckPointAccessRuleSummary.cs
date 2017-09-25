using Newtonsoft.Json;
using psCheckPoint.Session;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.AccessRule
{
    /// <summary>
    /// <para type="description">Details of a Check Point Access Rule</para>
    /// </summary>
    public class CheckPointAccessRuleSummary : CheckPointObject
    {
        /// <summary>
        /// JSON Constructor for Access Rule
        /// </summary>
        [JsonConstructor]
        protected CheckPointAccessRuleSummary(string name, string uID, string type, CheckPointDomain domain,
            bool enabled, string layer, int ruleNumber) :
            base(name, uID, type, domain)
        {
            Enabled = enabled;
            Layer = layer;
            RuleNumber = ruleNumber;
        }

        /// <summary>
        /// <para type="description">Enable/Disable the rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "enabled", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Enabled { get; private set; }

        /// <summary>
        /// <para type="description">N/A</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Layer { get; internal set; }

        /// <summary>
        /// <para type="description">Rule number in rulebase</para>
        /// </summary>
        [JsonProperty(PropertyName = "rule-number", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int RuleNumber { get; internal set; }

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <param name="Session">Current session used to get full details</param>
        /// <returns>Full details of object. If psCheckPoint doesn't implement the commands to get the full details of this object yet, returns this. If object not found then returns null.</returns>
        public override CheckPointObject ToFullObj(CheckPointSession Session)
        {
            return GetCheckPointObject<CheckPointAccessRule>(Session, "Get-CheckPointAccessRule", typeof(GetCheckPointAccessRule));
        }

        private T GetCheckPointObject<T>(CheckPointSession Session, string psCmdletName, Type psCmdlet) where T : CheckPointObject
        {
            if (this is T) { return this as T; }

            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo(psCmdletName, psCmdlet));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", this.UID);
                PSI.AddParameter("Layer", this.Layer);

                Collection<T> results = PSI.Invoke<T>();

                // Because the standard API call show-access-rule does not return rule number
                // Keep rule number from summary if it exists from API call show-access-rulebase.
                CheckPointObject obj = results.First();
                if (obj is CheckPointAccessRuleSummary)
                {
                    CheckPointAccessRuleSummary rule = (obj as CheckPointAccessRuleSummary);
                    if (rule.RuleNumber == 0)
                    {
                        rule.RuleNumber = this.RuleNumber;
                    }
                }

                return (T)obj;
            }
        }
    }
}