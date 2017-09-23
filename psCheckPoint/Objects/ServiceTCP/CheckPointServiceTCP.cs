using Newtonsoft.Json;
using psCheckPoint.Objects.Service;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="description">Details of a Check Point TCP Service</para>
    /// </summary>
    public class CheckPointServiceTCP : CheckPointServiceBase
    {
        [JsonConstructor]
        private CheckPointServiceTCP(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments, CheckPointObject[] groups, bool keepConnectionsOpenAfterPolicyInstallation, bool matchByProtocolSignature, bool matchForAny, bool overrideDefaultSettings, string port, string protocol, int sessionTimeout, string sourcePort, bool syncConnectionsOnCluster, bool useDefaultSessionTimeout) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments, groups, keepConnectionsOpenAfterPolicyInstallation, matchByProtocolSignature, matchForAny, overrideDefaultSettings, port, protocol, sessionTimeout, sourcePort, syncConnectionsOnCluster, useDefaultSessionTimeout)
        {
        }

        public override string ToString()
        {
            return $"{Name} (tcp/{Port})";
        }
    }
}