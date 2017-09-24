using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Service
{
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    public abstract class NewCheckPointService<T> : NewCheckPointObject<T>
    {
        //TODO aggressive-aging

        /// <summary>
        /// <para type="description">Keep connections open after policy has been installed even if they are not allowed under the new policy. This overrides the settings in the Connection Persistence page. If you change this property, the change will not affect open connections, but only future connections.</para>
        /// </summary>
        [JsonProperty(PropertyName = "keep-connections-open-after-policy-installation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter KeepConnectionsOpenAfterPolicyInstallation { get; set; }

        /// <summary>
        /// <para type="description">A value of true enables matching by the selected protocol's signature - the signature identifies the protocol as genuine. Select this option to limit the port to the specified protocol. If the selected protocol does not support matching by signature, this field cannot be set to true.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-by-protocol-signature", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter MatchByProtocolSignature { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is used when 'Any' is set as the rule's service and there are several service objects with the same source port and protocol.</para>
        /// </summary>
        [JsonProperty(PropertyName = "match-for-any", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter MatchForAny { get; set; }

        /// <summary>
        /// <para type="description">Indicates whether this service is a Data Domain service which has been overridden.</para>
        /// </summary>
        [JsonProperty(PropertyName = "override-default-settings", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter OverrideDefaultSettings { get; set; }

        /// <summary>
        /// <para type="description">The number of the port used to provide this service. To specify a port range, place a hyphen between the lowest and highest port numbers, for example 44-55.</para>
        /// </summary>
        [JsonProperty(PropertyName = "port")]
        [Parameter]
        public string Port { get; set; }

        /// <summary>
        /// <para type="description">Select the protocol type associated with the service, and by implication, the management server (if any) that enforces Content Security and Authentication for the service. Selecting a Protocol Type invokes the specific protocol handlers for each protocol type, thus enabling higher level of security by parsing the protocol, and higher level of connectivity by tracking dynamic actions (such as opening of ports).</para>
        /// </summary>
        [JsonProperty(PropertyName = "protocol")]
        [Parameter]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">Time (in seconds) before the session times out.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout")]
        [Parameter]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Port number for the client side service. If specified, only those Source port Numbers will be Accepted, Dropped, or Rejected during packet inspection. Otherwise, the source port is not inspected.</para>
        /// </summary>
        [JsonProperty(PropertyName = "source-port")]
        [Parameter]
        public string SourcePort { get; set; }

        /// <summary>
        /// <para type="description">Enables state-synchronised High Availability or Load Sharing on a ClusterXL or OPSEC-certified cluster.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sync-connections-on-cluster", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter SyncConnectionsOnCluster { get; set; }

        /// <summary>
        /// <para type="description">Use default virtual session timeout.</para>
        /// </summary>
        [JsonProperty(PropertyName = "use-default-session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter UseDefaultSessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups
        {
            get { return _groups; }
            set { _groups = CreateArray(value); }
        }

        private string[] _groups;
    }
}