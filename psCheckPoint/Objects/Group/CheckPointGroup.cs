using Newtonsoft.Json;

namespace psCheckPoint.Objects.Group
{
    /// <summary>
    /// <para type="description">Details of a Check Point Group</para>
    /// </summary>
    public class CheckPointGroup : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointGroup(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, CheckPointObject[] members) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            Members = members;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public CheckPointObject[] Groups { get; private set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "members", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public CheckPointObject[] Members { get; private set; }

        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }

        public bool ShouldSerializeMembers()
        {
            return Members.Length > 0;
        }

        protected override void Refresh(CheckPointObject obj)
        {
            base.Refresh(obj);
            CheckPointGroup o = (CheckPointGroup)obj;

            Groups = o.Groups;
            Members = o.Members;
        }
    }
}