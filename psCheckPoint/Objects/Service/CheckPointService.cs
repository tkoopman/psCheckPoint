using Newtonsoft.Json;
using System.ComponentModel;

namespace psCheckPoint.Objects.Service
{
    /// <summary>
    /// <para type="description">Base details of a Check Point Service</para>
    /// </summary>
    public abstract class CheckPointService : CheckPointObjectFull
    {
        [JsonConstructor]
        protected CheckPointService(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            CheckPointObject[] groups, bool keepConnectionsOpenAfterPolicyInstallation, bool matchByProtocolSignature, bool matchForAny, bool overrideDefaultSettings, string port, string protocol, int sessionTimeout, string sourcePort, bool syncConnectionsOnCluster, bool useDefaultSessionTimeout) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Groups = groups;
            KeepConnectionsOpenAfterPolicyInstallation = keepConnectionsOpenAfterPolicyInstallation;
            MatchByProtocolSignature = matchByProtocolSignature;
            MatchForAny = matchForAny;
            OverrideDefaultSettings = overrideDefaultSettings;
            Port = port;
            Protocol = protocol;
            SessionTimeout = sessionTimeout;
            SourcePort = sourcePort;
            SyncConnectionsOnCluster = syncConnectionsOnCluster;
            UseDefaultSessionTimeout = useDefaultSessionTimeout;
        }

        //TODO aggerssive-aging

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }

        /// <summary>
        /// <para type="description">Keep connections open after policy has been installed even if they are not allowed under the new policy. This overrides the settings in the Connection Persistence page. If you change this property, the change will not affect open connections, but only future connections.</para>
        /// </summary>
        [JsonProperty(PropertyName = "keep-connections-open-after-policy-installation", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool KeepConnectionsOpenAfterPolicyInstallation { get; private set; }

        /// <summary>
        /// <para type="description">A value of true enables matching by the selected protocol's signature - The signature identifies the protocol as genuine.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-by-protocol-signature", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool MatchByProtocolSignature { get; private set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is used when 'Any' is set as the rule's service and there are several service objects with the same source port and protocol.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-for-any", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool MatchForAny { get; private set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is a Data Domain service which has been overridden.</para>
        /// </summary>
        [JsonProperty(PropertyName = "override-default-settings", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool OverrideDefaultSettings { get; private set; }

        /// <summary>
        /// <para type="description">The number of the port used to provide this service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Port { get; private set; }

        /// <summary>
        /// <para type="description">The protocol type associated with the service, and by implication, the management server (if any) that enforces Content Security and Authentication for the service.</para>
        /// </summary>
        [JsonProperty(PropertyName = "protocol", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Protocol { get; private set; }

        /// <summary>
        /// <para type="description">Time (in seconds) before the session times out.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int SessionTimeout { get; private set; }

        /// <summary>
        /// <para type="description">Port number for the client side service. If specified, only those Source port Numbers will be Accepted, Dropped, or Rejected during packet inspection. Otherwise, the source port is not inspected.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source-port", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string SourcePort { get; private set; }

        /// <summary>
        /// <para type="description">Enables state-synchronized High Availability or Load Sharing on a ClusterXL or OPSEC-certified cluster.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sync-connections-on-cluster", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool SyncConnectionsOnCluster { get; private set; }

        /// <summary>
        /// <para type="description">Use default virtual session timeout.</para>
        /// </summary>
        [JsonProperty(PropertyName = "use-default-session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool UseDefaultSessionTimeout { get; private set; }

        public bool ShouldSerializeGroups()
        {
            return Groups.Length > 0;
        }
    }
}