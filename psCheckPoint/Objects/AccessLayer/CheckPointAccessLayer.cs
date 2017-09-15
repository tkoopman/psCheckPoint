using Newtonsoft.Json;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <summary>
    /// <para type="description">Details of a Check Point Access Layer</para>
    /// </summary>
    public class CheckPointAccessLayer : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointAccessLayer(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            bool applicationsAndUrlFiltering, bool contentAwareness, bool detectUsingXForwardFor, bool firewall, bool mobileAccess, string parentLayer, bool shared) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            ApplicationsAndUrlFiltering = applicationsAndUrlFiltering;
            ContentAwareness = contentAwareness;
            DetectUsingXForwardFor = detectUsingXForwardFor;
            Firewall = firewall;
            MobileAccess = mobileAccess;
            ParentLayer = parentLayer;
            Shared = shared;
        }

        /// <summary>
        /// <para type="description">Whether Applications and URL Filtering blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "applications-and-url-filtering", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool ApplicationsAndUrlFiltering { get; private set; }

        /// <summary>
        /// <para type="description">Whether Content Awareness blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "content-awareness", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool ContentAwareness { get; private set; }

        /// <summary>
        /// <para type="description">Whether X-Forward-For HTTP header is been used.</para>
        /// </summary>
        [JsonProperty(PropertyName = "detect-using-x-forward-for", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool DetectUsingXForwardFor { get; private set; }

        /// <summary>
        /// <para type="description">Whether Firewall blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "firewall", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool Firewall { get; private set; }

        /// <summary>
        /// <para type="description">Whether Mobile Access blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mobile-access", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool MobileAccess { get; private set; }

        /// <summary>
        /// <para type="description">Parent layer of this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "parent-layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string ParentLayer { get; private set; }

        /// <summary>
        /// <para type="description">Whether this layer is shared.</para>
        /// </summary>
        [JsonProperty(PropertyName = "shared", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool Shared { get; private set; }
    }
}