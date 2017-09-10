﻿using Newtonsoft.Json;

namespace psCheckPoint.Objects.Service
{
    /// <summary>
    /// <para type="description">Base details of a Check Point Service</para>
    /// </summary>
    public abstract class CheckPointService : CheckPointObjectFull
    {
        //TODO aggerssive-aging

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public CheckPointObject[] Groups { get; set; }

        /// <summary>
        /// <para type="description">Keep connections open after policy has been installed even if they are not allowed under the new policy. This overrides the settings in the Connection Persistence page. If you change this property, the change will not affect open connections, but only future connections.</para>
        /// </summary>
        [JsonProperty(PropertyName = "keep-connections-open-after-policy-installation")]
        public bool KeepConnectionsOpenAfterPolicyInstallation { get; set; }

        /// <summary>
        /// <para type="description">A value of true enables matching by the selected protocol's signature - The signature identifies the protocol as genuine.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-by-protocol-signature")]
        public bool MatchByProtocolSignature { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is used when 'Any' is set as the rule's service and there are several service objects with the same source port and protocol.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-for-any")]
        public bool MatchForAny { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is a Data Domain service which has been overridden.</para>
        /// </summary>
        [JsonProperty(PropertyName = "override-default-settings")]
        public bool OverrideDefaultSettings { get; set; }

        /// <summary>
        /// <para type="description">The number of the port used to provide this service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "port")]
        public string Port { get; set; }

        /// <summary>
        /// <para type="description">The protocol type associated with the service, and by implication, the management server (if any) that enforces Content Security and Authentication for the service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">Time (in seconds) before the session times out.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout")]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Port number for the client side service. If specified, only those Source port Numbers will be Accepted, Dropped, or Rejected during packet inspection. Otherwise, the source port is not inspected.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source-port")]
        public string SourcePort { get; set; }

        /// <summary>
        /// <para type="description">Enables state-synchronized High Availability or Load Sharing on a ClusterXL or OPSEC-certified cluster.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sync-connections-on-cluster")]
        public bool SyncConnectionsOnCluster { get; set; }

        /// <summary>
        /// <para type="description">Use default virtual session timeout.</para>
        /// </summary>
        [JsonProperty(PropertyName = "use-default-session-timeout")]
        public bool UseDefaultSessionTimeout { get; set; }
    }
}