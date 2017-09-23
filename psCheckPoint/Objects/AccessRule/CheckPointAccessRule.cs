using Newtonsoft.Json;
using System.ComponentModel;

namespace psCheckPoint.Objects.AccessRule
{
    /// <summary>
    /// <para type="description">Details of a Check Point Access Rule</para>
    /// </summary>
    public class CheckPointAccessRule : CheckPointObject
    {
        [JsonConstructor]
        private CheckPointAccessRule(string name, string uID, string type, CheckPointDomain domain,
            CheckPointObject action, CheckPointObject[] destination, bool destinationNegate, bool enabled, CheckPointObject inlineLayer, CheckPointObject[] installOn, string layer, CheckPointMetaInfo metaInfo, CheckPointObject[] service, bool serviceNegate, CheckPointObject[] source, bool sourceNegate, CheckPointObject[] time, CheckPointObject[] vPN, string comments) :
            base(name, uID, type, domain)
        {
            Action = action;
            Destination = destination;
            DestinationNegate = destinationNegate;
            Enabled = enabled;
            InlineLayer = inlineLayer;
            InstallOn = installOn;
            Layer = layer;
            MetaInfo = metaInfo;
            Service = service;
            ServiceNegate = serviceNegate;
            Source = source;
            SourceNegate = sourceNegate;
            Time = time;
            VPN = vPN;
            Comments = comments;
        }

        /// <summary>
        /// <para type="description">"Accept", "Drop", "Ask", "Inform", "Reject", "User Auth", "Client Auth", "Apply Layer". How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "action", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Action { get; private set; }

        //TODO action-settings
        //TODO content
        //TODO content-direction
        //TODO content-negate
        //TODO custom-fields

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "destination", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Destination { get; private set; }

        /// <summary>
        /// <para type="description">True if negate is set for destination.</para>
        /// </summary>
        [JsonProperty(PropertyName = "destination-negate", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool DestinationNegate { get; private set; }

        /// <summary>
        /// <para type="description">Enable/Disable the rule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "enabled", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Enabled { get; private set; }

        //TODO hits

        /// <summary>
        /// <para type="description">Inline Layer identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "inline-layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject InlineLayer { get; private set; }

        /// <summary>
        /// <para type="description">Which gateway, identified by the name or UID, to install the policy. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "install-on", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] InstallOn { get; private set; }

        /// <summary>
        /// <para type="description">N/A</para>
        /// </summary>
        [JsonProperty(PropertyName = "layer", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Layer { get; private set; }

        /// <summary>
        /// <para type="description">Object metadata.</para>
        /// </summary>
        [JsonProperty(PropertyName = "meta-info", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointMetaInfo MetaInfo { get; private set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "service", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Service { get; private set; }

        /// <summary>
        /// <para type="description">True if negate is set for service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "service-negate", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool ServiceNegate { get; private set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Source { get; private set; }

        /// <summary>
        /// <para type="description">True if negate is set for source.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source-negate", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool SourceNegate { get; private set; }

        /// <summary>
        /// <para type="description">List of time objects. For example: "Weekend", "Off-Work", "Every-Day". How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Time { get; private set; }

        //TODO track
        //TODO user-check

        /// <summary>
        /// <para type="description">VPN settings. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "vpn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] VPN { get; private set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Comments { get; private set; }
    }
}