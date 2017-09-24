using Newtonsoft.Json;
using System.ComponentModel;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Extended summary details of most Check Point Objects</para>
    /// </summary>
    public abstract class CheckPointObjectFull : CheckPointObject
    {
        /// <summary>
        /// JSON Constructor for Common Check Point Object Details
        /// </summary>
        [JsonConstructor]
        protected CheckPointObjectFull(string name, string uID, string type, CheckPointDomain domain,
            string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments) :
            base(name, uID, type, domain)
        {
            Icon = icon;
            MetaInfo = metaInfo;
            ReadOnly = readOnly;
            Tags = tags;
            Color = color;
            Comments = comments;
        }

        /// <summary>
        /// <para type="description">Object icon.</para>
        /// </summary>
        [JsonProperty(PropertyName = "icon", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 999)]
        [DefaultValue("")]
        public string Icon { get; private set; }

        /// <summary>
        /// <para type="description">Object metadata.</para>
        /// </summary>
        [JsonProperty(PropertyName = "meta-info", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 901)]
        public CheckPointMetaInfo MetaInfo { get; private set; }

        /// <summary>
        /// <para type="description">Indicates whether the object is read-only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 12)]
        [DefaultValue(false)]
        public bool ReadOnly { get; private set; }

        /// <summary>
        /// <para type="description">Collection of tag objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 11)]
        public CheckPointObject[] Tags { get; private set; }

        /// <summary>
        /// <para type="description">Color of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "color", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 10)]
        public string Color { get; private set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 800)]
        [DefaultValue("")]
        public string Comments { get; private set; }

        /// <summary>
        /// Conditional Property Serialization for Tags
        /// </summary>
        /// <returns>true if Tags should be serialised.</returns>
        public bool ShouldSerializeTags()
        {
            return Tags.Length > 0;
        }

        /// <summary>
        /// Conditional Property Serialization for Color
        /// </summary>
        /// <returns>true if Color should be serialised.</returns>
        public bool ShouldSerializeColor()
        {
            return !(string.IsNullOrWhiteSpace(Color) || Color.Equals("black"));
        }
    }
}