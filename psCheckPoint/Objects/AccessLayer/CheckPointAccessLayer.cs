using Newtonsoft.Json;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <summary>
    /// <para type="description">Details of a Check Point Access Layer</para>
    /// </summary>
    public class CheckPointAccessLayer : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">Whether Applications and URL Filtering blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "applications-and-url-filtering")]
        public bool ApplicationsAndUrlFiltering { get; set; }

        /// <summary>
        /// <para type="description">Whether Content Awareness blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "content-awareness")]
        public bool ContentAwareness { get; set; }

        /// <summary>
        /// <para type="description">Whether X-Forward-For HTTP header is been used.</para>
        /// </summary>
        [JsonProperty(PropertyName = "detect-using-x-forward-for")]
        public bool DetectUsingXForwardFor { get; set; }

        /// <summary>
        /// <para type="description">Whether Firewall blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "firewall")]
        public bool Firewall { get; set; }

        /// <summary>
        /// <para type="description">Whether Mobile Access blade is enabled on this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mobile-access")]
        public bool MobileAccess { get; set; }

        /// <summary>
        /// <para type="description">Parent layer of this layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "parent-layer")]
        public string ParentLayer { get; set; }

        /// <summary>
        /// <para type="description">Whether this layer is shared.</para>
        /// </summary>
        [JsonProperty(PropertyName = "shared")]
        public bool Shared { get; set; }
    }
}