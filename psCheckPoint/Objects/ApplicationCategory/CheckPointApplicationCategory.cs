using Newtonsoft.Json;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <summary>
    /// <para type="description">Details of a Check Point Application Category</para>
    /// </summary>
    public class CheckPointApplicationCategory : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointApplicationCategory(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, string description) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            Description = description;
        }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        /// <summary>
        /// <para type="description">Application description.</para>
        /// </summary>
        [JsonProperty(PropertyName = "description", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; private set; }
    }
}