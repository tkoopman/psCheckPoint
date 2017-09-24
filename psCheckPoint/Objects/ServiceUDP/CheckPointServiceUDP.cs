using Newtonsoft.Json;
using psCheckPoint.Objects.Service;
using System.ComponentModel;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <summary>
    /// <para type="description">Details of a Check Point UDP Service</para>
    /// </summary>
    public class CheckPointServiceUDP : CheckPointServiceBase
    {
        /// <summary>
        /// JSON Constructor for UDP Service
        /// </summary>
        [JsonConstructor]
        private CheckPointServiceUDP(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments, CheckPointObject[] groups, bool keepConnectionsOpenAfterPolicyInstallation, bool matchByProtocolSignature, bool matchForAny, bool overrideDefaultSettings, string port, string protocol, int sessionTimeout, string sourcePort, bool syncConnectionsOnCluster, bool useDefaultSessionTimeout,
            bool acceptReplies) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments, groups, keepConnectionsOpenAfterPolicyInstallation, matchByProtocolSignature, matchForAny, overrideDefaultSettings, port, protocol, sessionTimeout, sourcePort, syncConnectionsOnCluster, useDefaultSessionTimeout)
        {
            AcceptReplies = acceptReplies;
        }

        /// <summary>
        /// Converts UDP port to a string
        /// </summary>
        /// <returns>String of UDP port</returns>
        public override string ToString()
        {
            return $"{Name} (udp/{Port})";
        }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "accept-replies", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AcceptReplies { get; private set; }
    }
}