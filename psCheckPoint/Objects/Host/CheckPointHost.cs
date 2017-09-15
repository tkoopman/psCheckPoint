using Newtonsoft.Json;

namespace psCheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="description">Details of a Check Point Host</para>
    /// </summary>
    public class CheckPointHost : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointHost(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, string ipv4Address, string ipv6Address) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            IPv4Address = ipv4Address;
            IPv6Address = ipv6Address;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        // TODO interfaces

        /// <summary>
        /// <para type="description">IPv4 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv4Address { get; private set; }

        /// <summary>
        /// <para type="description">IPv6 host address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPv6Address { get; private set; }

        //TODO nat-settings
        //TODO host-servers

        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }

        protected override void Refresh(CheckPointObject obj)
        {
            base.Refresh(obj);
            CheckPointHost o = (CheckPointHost)obj;

            Groups = o.Groups;
            IPv4Address = o.IPv4Address;
            IPv6Address = o.IPv6Address;
        }
    }
}