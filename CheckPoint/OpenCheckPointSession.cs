using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CheckPoint
{
    /// <summary>
    /// <para type="synopsis">Response from Open-CheckPointSession</para>
    /// <para type="description">Used across other Check Point Web API Calls</para>
    /// </summary>
    public class CheckPointSession
    {
        /// <summary>
        /// <para type="description">Session unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sid")]
        public string SID { get; set; }

        /// <summary>
        /// <para type="description">API Server version.</para>
        /// </summary>
        [JsonProperty(PropertyName = "api-server-version")]
        public string APIServerVersion { get; set; }

        /// <summary>
        /// <para type="description">Information about the available disk space on the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "disk-space-message")]
        public string DiskSpaceMessage { get; set; }

        /// <summary>
        /// <para type="description">True if this session is read only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout")]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">True if this management server is in the standby mode.</para>
        /// </summary>
        [JsonProperty(PropertyName = "standby")]
        public bool Standby { get; set; }

        /// <summary>
        /// <para type="description">Session object unique identifier. This identifier may be used in the discard API to discard changes that were made in this session, when administrator is working from another session, or in the 'switch-session' API.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">URL that was used to reach the API server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Log in to the server with username and password.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$Session = Open-CheckPointSession -ManagementServer 192.168.1.1</code>
    /// </example>
    [JsonObject(MemberSerialization.OptIn)]
    [Cmdlet(VerbsCommon.Open, "CheckPointSession")]
    [OutputType(typeof(CheckPointSession))]
    public class OpenCheckPointSession : Cmdlet
    {
        /// <summary>
        /// <para type="description">IP or Hostname of the Check point Management Server</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string ManagementServer { get; set; }

        /// <summary>
        /// <para type="description">Port Web API running on. Default: 443</para>
        /// </summary>
        [Parameter]
        public int ManagementPort { get; set; } = 443;

        /// <summary>
        /// <para type="description">PSCredential containing Username and Password. If not provided you will be prompted.</para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public PSCredential Credentials { get; set; }

        [JsonProperty(PropertyName = "user")]
        private string User { get; set; }

        [JsonProperty(PropertyName = "password")]
        private string Password { get; set; }

        /// <summary>
        /// <para type="description">Login with Read Only permissions. This parameter is not considered in case continue-last-session is true.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter ReadOnly { get; set; }

        /// <summary>
        /// <para type="description">The new session would continue where the last session was stopped.</para>
        /// <para type="description">This option is available when the administrator has only one session that can be continued.</para>
        /// <para type="description">If there is more than one session, see 'switch-session' API.</para>
        /// </summary>
        [JsonProperty(PropertyName = "continue-last-session", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter ContinueLastSession { get; set; }

        /// <summary>
        /// <para type="description">Use domain to login to specific domain. Domain can be identified by name or UID.</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">Login to the last published session. Such login is done with the Read Only permissions.</para>
        /// </summary>
        [JsonProperty(PropertyName = "enter-last-published-session", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter EnterLastPublishedSession { get; set; }

        /// <summary>
        /// <para type="description">Session comments.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-comments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public string SessionComments { get; set; }

        /// <summary>
        /// <para type="description">Session description.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public string SessionDescription { get; set; }

        /// <summary>
        /// <para type="description">Session unique name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public string SessionName { get; set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds. Default 600 seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Do NOT verify server's certificate.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoCertificateValidation { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            User = Credentials.GetNetworkCredential().UserName;

            // Debug Output Request
            Password = "***";
            string strJson = JsonConvert.SerializeObject(this);
            this.WriteDebug($@"JSON Request to https://{ManagementServer}:{ManagementPort}/web_api/login
{strJson}");

            Password = Credentials.GetNetworkCredential().Password;
            strJson = JsonConvert.SerializeObject(this);

            try
            {
                if (NoCertificateValidation.IsPresent)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback( delegate { return true; } );
                } else
                {
                    ServicePointManager.ServerCertificateValidationCallback = null;
                }
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync($"https://{ManagementServer}:{ManagementPort}/web_api/login", new StringContent(strJson, Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    strJson = response.Content.ReadAsStringAsync().Result;

                    // Debug Output Request
                    this.WriteDebug($@"JSON Response
{strJson}");

                    CheckPointSession session = JsonConvert.DeserializeObject<CheckPointSession>(strJson);
                    WriteObject(session);
                }
                else
                {
                    WriteWarning($"Server returned status code: {(int)response.StatusCode} [{response.StatusCode}]");
                    strJson = response.Content.ReadAsStringAsync().Result;
                    WriteDebug(strJson);
                    CheckPointError error = JsonConvert.DeserializeObject<CheckPointError>(strJson);
                    WriteObject(error);
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                this.WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}