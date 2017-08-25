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

        //TODO meta-info

        /// <summary>
        /// <para type="description">Indicates whether the object is read-only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only")]
        public bool ReadOnly { get; set; }

        //TODO tags

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