using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SimpleGateway
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPoint_, Get-CheckPoint_ & Get-CheckPoint_s</para>
    /// <para type="description">_ object details.</para>
    /// </summary>
    public class CheckPointSimpleGateway : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointSimpleGateway(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            bool antiBot, bool antiVirus, bool applicationControl, bool contentAwareness, bool dynamicIP, bool firewall, string hardware, bool iPS, string pv4Address, string pv6Address, string oSName, bool saveLogsLocally, string[] sendAlertsToServer, string[] sendLogsToBackupServer, string[] sendLogsToServer, string sICName, string sICState, bool threatEmulation, bool urlFiltering, string version, bool vPN, CheckPointObject[] groups) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            AntiBot = antiBot;
            AntiVirus = antiVirus;
            ApplicationControl = applicationControl;
            ContentAwareness = contentAwareness;
            DynamicIP = dynamicIP;
            Firewall = firewall;
            Hardware = hardware;
            IPS = iPS;
            IPv4Address = pv4Address;
            IPv6Address = pv6Address;
            OSName = oSName;
            SaveLogsLocally = saveLogsLocally;
            SendAlertsToServer = sendAlertsToServer;
            SendLogsToBackupServer = sendLogsToBackupServer;
            SendLogsToServer = sendLogsToServer;
            SICName = sICName;
            SICState = sICState;
            ThreatEmulation = threatEmulation;
            UrlFiltering = urlFiltering;
            Version = version;
            VPN = vPN;
            Groups = groups;
        }

        /// <summary>
        /// <para type="description">Anti-Bot blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "anti-bot", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AntiBot { get; private set; }

        /// <summary>
        /// <para type="description">Anti-Virus blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "anti-virus", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool AntiVirus { get; private set; }

        /// <summary>
        /// <para type="description">Application Control blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "application-control", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool ApplicationControl { get; private set; }

        /// <summary>
        /// <para type="description">Content Awareness blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "content-awareness", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool ContentAwareness { get; private set; }

        /// <summary>
        /// <para type="description">Dynamic IP address</para>
        /// </summary>
        [JsonProperty(PropertyName = "dynamic-ip", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool DynamicIP { get; private set; }

        /// <summary>
        /// <para type="description">Firewall blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "firewall", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Firewall { get; private set; }

        //TODO firewall-settings

        /// <summary>
        /// <para type="description">Gateway platform hardware type.</para>
        /// </summary>
        [JsonProperty(PropertyName = "hardware", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Hardware { get; private set; }

        //TODO interfaces

        /// <summary>
        /// <para type="description">Intrusion Prevention System blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ips", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool IPS { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string IPv4Address { get; private set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string IPv6Address { get; private set; }

        //TODO logs-settings

        /// <summary>
        /// <para type="description">Gateway platform operating system.</para>
        /// </summary>
        [JsonProperty(PropertyName = "os-name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string OSName { get; private set; }

        /// <summary>
        /// <para type="description">Save logs locally on the gateway.</para>
        /// </summary>
        [JsonProperty(PropertyName = "save-logs-locally", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool SaveLogsLocally { get; private set; }

        /// <summary>
        /// <para type="description">Server(s) to send alerts to.</para>
        /// </summary>
        [JsonProperty(PropertyName = "send-alerts-to-server", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] SendAlertsToServer { get; private set; }

        /// <summary>
        /// <para type="description">Backup server(s) to send logs to.</para>
        /// </summary>
        [JsonProperty(PropertyName = "send-logs-to-backup-server", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] SendLogsToBackupServer { get; private set; }

        /// <summary>
        /// <para type="description">Servers(s) to send logs to.</para>
        /// </summary>
        [JsonProperty(PropertyName = "send-logs-to-server", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string[] SendLogsToServer { get; private set; }

        /// <summary>
        /// <para type="description">Secure Internal Communication name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sic-name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string SICName { get; private set; }

        /// <summary>
        /// <para type="description">Secure Internal Communication state.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sic-state", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string SICState { get; private set; }

        /// <summary>
        /// <para type="description">Threat Emulation blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "threat-emulation", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool ThreatEmulation { get; private set; }

        /// <summary>
        /// <para type="description">URL Filtering blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "url-filtering", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool UrlFiltering { get; private set; }

        /// <summary>
        /// <para type="description">Gateway platform version.</para>
        /// </summary>
        [JsonProperty(PropertyName = "version", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("")]
        public string Version { get; private set; }

        /// <summary>
        /// <para type="description">VPN blade enabled.</para>
        /// </summary>
        [JsonProperty(PropertyName = "vpn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool VPN { get; private set; }

        //TODO vpn-settings

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; private set; }
    }
}