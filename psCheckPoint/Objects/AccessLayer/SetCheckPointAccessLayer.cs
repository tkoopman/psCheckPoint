using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="set-access-layer">Set-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointAccessLayer")]
    [OutputType(typeof(CheckPointAccessLayer))]
    public class SetCheckPointAccessLayer : SetCheckPointObject<CheckPointAccessLayer>
    {
        public override string Command { get { return "set-access-layer"; } }

        /// <summary>
        /// <para type="description">Whether to enable Applications and URL Filtering blade on the layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "applications-and-url-filtering", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool ApplicationsAndUrlFiltering { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether to enable Content Awareness blade on the layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "content-awareness", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool ContentAwareness { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether to use X-Forward-For HTTP header, which is added by the proxy server to keep track of the original source IP.</para>
        /// </summary>
        [JsonProperty(PropertyName = "detect-using-x-forward-for", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool DetectUsingXForwardFor { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether to enable Firewall blade on the layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "firewall", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(true)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Firewall { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to enable Mobile Access blade on the layer.</para>
        /// </summary>
        [JsonProperty(PropertyName = "mobile-access", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool MobileAccess { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether this layer is shared.</para>
        /// </summary>
        [JsonProperty(PropertyName = "shared", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Shared { get; set; } = false;
    }
}