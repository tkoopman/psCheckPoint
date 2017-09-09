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
    [JsonObject(MemberSerialization.OptIn)]
    public class CheckPointExportSet
    {
        [JsonProperty(PropertyName = "AccessRules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAccessRule> AccessRules { get; set; } = new List<CheckPointAccessRule>();

        [JsonProperty(PropertyName = "AddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAddressRange> AddressRanges { get; set; } = new List<CheckPointAddressRange>();

        [JsonProperty(PropertyName = "Groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroup> Groups { get; set; } = new List<CheckPointGroup>();

        [JsonProperty(PropertyName = "GroupsWithExclusion", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroupWithExclusion> GroupsWithExclusion { get; set; } = new List<CheckPointGroupWithExclusion>();

        [JsonProperty(PropertyName = "Hosts", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointHost> Hosts { get; set; } = new List<CheckPointHost>();

        [JsonProperty(PropertyName = "MulticastAddressRanges", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointMulticastAddressRange> MulticastAddressRanges { get; set; } = new List<CheckPointMulticastAddressRange>();

        [JsonProperty(PropertyName = "Networks", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointNetwork> Networks { get; set; } = new List<CheckPointNetwork>();

        [JsonProperty(PropertyName = "Services", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointService> Services { get; set; } = new List<CheckPointService>();

        [JsonProperty(PropertyName = "ServiceGroups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointServiceGroup> ServiceGroups { get; set; } = new List<CheckPointServiceGroup>();

        [JsonProperty(PropertyName = "Other", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointObject> Other { get; set; } = new List<CheckPointObject>();
    }
}