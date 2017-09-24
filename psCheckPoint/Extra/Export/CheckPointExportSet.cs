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
        public List<CheckPointAccessRule> AccessRules { get; private set; } = new List<CheckPointAccessRule>();

        /// <summary>
        /// <para type="synopsis">List of exported Address Ranges.</para>
        /// </summary>
        [JsonProperty(PropertyName = "AddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAddressRange> AddressRanges { get; private set; } = new List<CheckPointAddressRange>();

        /// <summary>
        /// <para type="synopsis">List of exported Groups.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroup> Groups { get; private set; } = new List<CheckPointGroup>();

        /// <summary>
        /// <para type="synopsis">List of exported Groups with Exclusion.</para>
        /// </summary>
        [JsonProperty(PropertyName = "GroupsWithExclusion", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroupWithExclusion> GroupsWithExclusion { get; private set; } = new List<CheckPointGroupWithExclusion>();

        /// <summary>
        /// <para type="synopsis">List of exported Hosts.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Hosts", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointHost> Hosts { get; private set; } = new List<CheckPointHost>();

        /// <summary>
        /// <para type="synopsis">List of exported Multicast Address Ranges.</para>
        /// </summary>
        [JsonProperty(PropertyName = "MulticastAddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointMulticastAddressRange> MulticastAddressRanges { get; private set; } = new List<CheckPointMulticastAddressRange>();

        /// <summary>
        /// <para type="synopsis">List of exported Networks.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Networks", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointNetwork> Networks { get; private set; } = new List<CheckPointNetwork>();

        /// <summary>
        /// <para type="synopsis">List of exported Services.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Services", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointServiceBase> Services { get; private set; } = new List<CheckPointServiceBase>();

        /// <summary>
        /// <para type="synopsis">List of exported Service Groups.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ServiceGroups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointServiceGroup> ServiceGroups { get; private set; } = new List<CheckPointServiceGroup>();

        /// <summary>
        /// <para type="synopsis">List of exported objects not currently fully implemented in psCheckPoint.</para>
        /// </summary>
        [JsonProperty(PropertyName = "Other", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointObject> Other { get; private set; } = new List<CheckPointObject>();

        /// <summary>
        /// Conditional Property Serialization for Rules
        /// </summary>
        /// <returns>true if Rules should be serialised.</returns>
        public bool ShouldSerializeAccessRules()
        {
            return AccessRules.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Address Ranges
        /// </summary>
        /// <returns>true if AddressRanges should be serialised.</returns>
        public bool ShouldSerializeAddressRanges()
        {
            return AddressRanges.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Groups
        /// </summary>
        /// <returns>true if Groups should be serialised.</returns>
        public bool ShouldSerializeGroups()
        {
            return Groups.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Groups with Exclusion
        /// </summary>
        /// <returns>true if GroupsWithExclusion should be serialised.</returns>
        public bool ShouldSerializeGroupsWithExclusion()
        {
            return GroupsWithExclusion.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Hosts
        /// </summary>
        /// <returns>true if Hosts should be serialised.</returns>
        public bool ShouldSerializeHosts()
        {
            return Hosts.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Multicast Address Ranges
        /// </summary>
        /// <returns>true if MulticastAddressRanges should be serialised.</returns>
        public bool ShouldSerializeMulticastAddressRanges()
        {
            return MulticastAddressRanges.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Networks
        /// </summary>
        /// <returns>true if Networks should be serialised.</returns>
        public bool ShouldSerializeNetworks()
        {
            return Networks.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Services
        /// </summary>
        /// <returns>true if Services should be serialised.</returns>
        public bool ShouldSerializeServices()
        {
            return Services.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Service Groups
        /// </summary>
        /// <returns>true if ServiceGroups should be serialised.</returns>
        public bool ShouldSerializeServiceGroups()
        {
            return ServiceGroups.Count > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Other
        /// </summary>
        /// <returns>true if Other should be serialised.</returns>
        public bool ShouldSerializeOther()
        {
            return Other.Count > 0;
        }
    }
}