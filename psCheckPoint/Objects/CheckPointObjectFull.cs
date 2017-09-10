using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Extended summary details of most Check Point Objects</para>
    /// </summary>
    public class CheckPointObjectFull : CheckPointObject
    {
        /// <summary>
        /// <para type="description">Object icon.</para>
        /// </summary>
        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        /// <summary>
        /// <para type="description">Object metadata.</para>
        /// </summary>
        [JsonProperty(PropertyName = "meta-info", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointMetaInfo MetaInfo { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether the object is read-only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// <para type="description">Collection of tag objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Tags { get; set; }

        /// <summary>
        /// <para type="description">Color of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Ignore)]
        public string Comments { get; set; }
    }
}