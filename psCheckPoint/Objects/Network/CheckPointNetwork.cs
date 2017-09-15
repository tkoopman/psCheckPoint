using Newtonsoft.Json;

namespace psCheckPoint.Objects.Network
{
    /// <summary>
    /// <para type="description">Details of a Check Point Network</para>
    /// </summary>
    public class CheckPointNetwork : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointNetwork(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, string subnet4, string subnet6, string broadcast, int maskLength4, int maskLength6, string subnetMask) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            Subnet4 = subnet4;
            Subnet6 = subnet6;
            Broadcast = broadcast;
            MaskLength4 = maskLength4;
            MaskLength6 = maskLength6;
            SubnetMask = subnetMask;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        //TODO host-servers
        //TODO nat-settings

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Subnet4 { get; private set; }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Subnet6 { get; private set; }

        /// <summary>
        /// <para type="description">Allow broadcast address inclusion.</para>
        /// </summary>
        [JsonProperty(PropertyName = "broadcast", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Broadcast { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int MaskLength4 { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mask-length6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int MaskLength6 { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 network mask.</para>
        /// </summary>
        [JsonProperty(PropertyName = "subnet-mask", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string SubnetMask { get; private set; }

        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }

        protected override void Refresh(CheckPointObject obj)
        {
            base.Refresh(obj);
            CheckPointNetwork o = (CheckPointNetwork)obj;

            Groups = o.Groups;
            Subnet4 = o.Subnet4;
            Subnet6 = o.Subnet6;
            Broadcast = o.Broadcast;
            MaskLength4 = o.MaskLength4;
            MaskLength6 = o.MaskLength6;
            SubnetMask = o.SubnetMask;
        }
    }
}