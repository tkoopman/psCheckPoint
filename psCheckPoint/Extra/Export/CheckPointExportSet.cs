using Newtonsoft.Json;
using psCheckPoint.Objects;
using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Objects.AddressRange;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.GroupWithExclusion;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.MulticastAddressRange;
using psCheckPoint.Objects.Network;
using psCheckPoint.Objects.Service;
using psCheckPoint.Objects.ServiceGroup;
using System.Collections.Generic;

namespace psCheckPoint.Extra.Export
{
    /// <summary>
    /// <para type="synopsis">Result of Export-CheckPointObjects.</para>
    /// <para type="description">Contains arrays of all exported objects including rules, groups, hosts, etc</para>
    /// <para type="description">Any unknown exported object will have summary in "Other" array</para>
    /// <para type="description">Pipe this to other commands like ConvertTo-CheckPointHtml or ConvertToJson for final export results</para>
    /// </summary>
    /// <example>
    /// <code>$Export | ConvertTo-CheckPointHtml -Open</code>
    /// </example>
    [JsonObject(MemberSerialization.OptIn)]
    public class CheckPointExportSet
    {
        /// <summary>
        /// <para type="synopsis">List of exported Access Rules.</para>
        /// </summary>
        [JsonProperty(PropertyName = "AccessRules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAccessRule> AccessRules { get; set; } = new List<CheckPointAccessRule>();

        /// <summary>
        /// <para type="synopsis">List of exported Address Ranges.</para>
        /// </summary>
        [JsonProperty(PropertyName = "AddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAddressRange> AddressRanges { get; set; } = new List<CheckPointAddressRange>();

        /// <summary>
        /// <para type="synopsis">List of exported Groups.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroup> Groups { get; set; } = new List<CheckPointGroup>();

        /// <summary>
        /// <para type="synopsis">List of exported Groups with Exclusion.</para>
        /// </summary>
        [JsonProperty(PropertyName = "GroupsWithExclusion", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroupWithExclusion> GroupsWithExclusion { get; set; } = new List<CheckPointGroupWithExclusion>();

        /// <summary>
        /// <para type="synopsis">List of exported Hosts.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Hosts", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointHost> Hosts { get; set; } = new List<CheckPointHost>();

        /// <summary>
        /// <para type="synopsis">List of exported Multicast Address Ranges.</para>
        /// </summary>
        [JsonProperty(PropertyName = "MulticastAddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointMulticastAddressRange> MulticastAddressRanges { get; set; } = new List<CheckPointMulticastAddressRange>();

        /// <summary>
        /// <para type="synopsis">List of exported Networks.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Networks", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointNetwork> Networks { get; set; } = new List<CheckPointNetwork>();

        /// <summary>
        /// <para type="synopsis">List of exported Services.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Services", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointService> Services { get; set; } = new List<CheckPointService>();

        /// <summary>
        /// <para type="synopsis">List of exported Service Groups.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ServiceGroups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointServiceGroup> ServiceGroups { get; set; } = new List<CheckPointServiceGroup>();

        /// <summary>
        /// <para type="synopsis">List of exported objects not currently fully implemented in psCheckPoint.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Other", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointObject> Other { get; set; } = new List<CheckPointObject>();
    }
}