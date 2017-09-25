using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace psCheckPoint.Objects.AccessRule
{
    /// <summary>
    /// <para type="description">Details of a Check Point Access Rule</para>
    /// </summary>
    public class CheckPointAccessRuleBase : CheckPointObjectsBase<CheckPointAccessRule>
    {
        /// <summary>
        /// JSON Constructor for Access Rule
        /// </summary>
        [JsonConstructor]
        private CheckPointAccessRuleBase(string name, string uID, List<CheckPointAccessRule> ruleBase)
        {
            Name = name;
            UID = uID;
            RuleBase = ruleBase;
        }

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = -100)]
        [DefaultValue("")]
        public string Name { get; private set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 900)]
        [DefaultValue("")]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">Rule base.</para>
        /// </summary>
        public List<CheckPointAccessRule> RuleBase { get { return _Objects; } private set { _Objects = value; } }
    }
}