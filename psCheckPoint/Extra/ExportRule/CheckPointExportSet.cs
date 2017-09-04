using Newtonsoft.Json;
using psCheckPoint.AccessControl_NAT.AccessRule;
using psCheckPoint.Objects;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.ExportRule
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CheckPointExportSet
    {
        [JsonProperty(PropertyName = "Rules", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointAccessRule> Rules { get; set; } = new List<CheckPointAccessRule>();

        [JsonProperty(PropertyName = "Groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointGroup> Groups { get; set; } = new List<CheckPointGroup>();

        [JsonProperty(PropertyName = "Hosts", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointHost> Hosts { get; set; } = new List<CheckPointHost>();

        [JsonProperty(PropertyName = "Other", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointObject> Other { get; set; } = new List<CheckPointObject>();
    }
}