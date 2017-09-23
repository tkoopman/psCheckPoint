using Newtonsoft.Json;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <summary>
    /// <para type="description">Details of a Check Point Group with Exclusion</para>
    /// </summary>
    public class CheckPointGroupWithExclusion : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointGroupWithExclusion(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject include, CheckPointObject except) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Include = include;
            Except = except;
        }

        /// <summary>
        /// <para type="description">Object which the group includes</para>
        /// </summary>
        [JsonProperty(PropertyName = "include", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Include { get; private set; }

        /// <summary>
        /// <para type="description">Object which the group excludes</para>
        /// </summary>
        [JsonProperty(PropertyName = "except", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Except { get; private set; }
    }
}