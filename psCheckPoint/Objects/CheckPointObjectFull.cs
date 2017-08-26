using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    public class CheckPointObjectFull : CheckPointObject
    {
        /// <summary>
        /// <para type="description">Object icon.</para>
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// <para type="description">Object metadata.</para>
        /// </summary>
        [JsonProperty(PropertyName = "meta-info")]
        public CheckPointMetaInfo MetaInfo { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether the object is read-only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// <para type="description">Collection of tag objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public CheckPointObject[] Tags { get; set; }

        /// <summary>
        /// <para type="description">Color of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
    }
}