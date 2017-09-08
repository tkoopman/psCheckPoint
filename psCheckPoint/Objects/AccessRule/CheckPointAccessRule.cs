using Newtonsoft.Json;
using psCheckPoint.Objects;

namespace psCheckPoint.Objects.AccessRule
{
    public class CheckPointAccessRule : CheckPointObject
    {
        /// <summary>
        /// <para type="description">"Accept", "Drop", "Ask", "Inform", "Reject", "User Auth", "Client Auth", "Apply Layer". How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public CheckPointObject Action { get; set; }

        //TODO action-settings
        //TODO content
        //TODO content-direction
        //TODO content-negate
        //TODO custom-fields

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "destination")]
        public CheckPointObject[] Destination { get; set; }

        /// <summary>
        /// <para type="description">True if negate is set for destination.</para>
        /// </summary>
        [JsonProperty(PropertyName = "destination-negate")]
        public bool DestinationNegate { get; set; }

        /// <summary>
        /// <para type="description">Enable/Disable the rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        //TODO hits

        /// <summary>
        /// <para type="description">Inline Layer identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "inline-layer")]
        public CheckPointObject InlineLayer { get; set; }

        /// <summary>
        /// <para type="description">Which gateway, identified by the name or UID, to install the policy. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "install-on")]
        public CheckPointObject[] InstallOn { get; set; }

        /// <summary>
        /// <para type="description">N/A</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer")]
        public string Layer { get; set; }

        /// <summary>
        /// <para type="description">Object metadata.</para>
        /// </summary>
        [JsonProperty(PropertyName = "meta-info")]
        public CheckPointMetaInfo MetaInfo { get; set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "service")]
        public CheckPointObject[] Service { get; set; }

        /// <summary>
        /// <para type="description">True if negate is set for service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "service-negate")]
        public bool ServiceNegate { get; set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public CheckPointObject[] Source { get; set; }

        /// <summary>
        /// <para type="description">True if negate is set for source.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source-negate")]
        public bool SourceNegate { get; set; }

        /// <summary>
        /// <para type="description">List of time objects. For example: "Weekend", "Off-Work", "Every-Day". How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public CheckPointObject[] Time { get; set; }

        //TODO track
        //TODO user-check

        /// <summary>
        /// <para type="description">VPN settings. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "vpn")]
        public CheckPointObject[] VPN { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }
    }
}