using Newtonsoft.Json;
using psCheckPoint.Objects;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Response from Open-CheckPointSession</para>
    /// <para type="description">Used across other Check Point Web API Calls</para>
    /// </summary>
    public class CheckPointSession
    {
        [JsonConstructor]
        private CheckPointSession(string SID, string APIServerVersion, string DiskSpaceMessage, CheckPointTime LastLoginWasAt, bool ReadOnly, int SessionTimeout, bool Standby, string UID, string URL)
        {
            this.SID = SID;
            this.APIServerVersion = APIServerVersion;
            this.DiskSpaceMessage = DiskSpaceMessage;
            this.LastLoginWasAt = LastLoginWasAt;
            this.ReadOnly = ReadOnly;
            this.SessionTimeout = SessionTimeout;
            this.Standby = Standby;
            this.UID = UID;
            this.URL = URL;
        }

        /// <summary>
        /// <para type="description">Session unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "sid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string SID { get; private set; }

        /// <summary>
        /// <para type="description">API Server version.</para>
        /// </summary>
        [JsonProperty(PropertyName = "api-server-version", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string APIServerVersion { get; private set; }

        /// <summary>
        /// <para type="description">Information about the available disk space on the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "disk-space-message", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string DiskSpaceMessage { get; private set; }

        /// <summary>
        /// <para type="description">Timestamp when administrator last accessed the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-login-was-at", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime LastLoginWasAt { get; private set; }

        //TODO login-message

        /// <summary>
        /// <para type="description">True if this session is read only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool ReadOnly { get; private set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int SessionTimeout { get; private set; }

        /// <summary>
        /// <para type="description">True if this management server is in the standby mode.</para>
        /// </summary>
        [JsonProperty(PropertyName = "standby", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool Standby { get; private set; }

        /// <summary>
        /// <para type="description">Session object unique identifier. This identifier may be used in the discard API to discard changes that were made in this session, when administrator is working from another session, or in the 'switch-session' API.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">URL that was used to reach the API server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "url", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string URL { get; private set; }

        internal bool EnableCompression { get; set; } = true;

        private HttpClient _httpClient = null;

        internal HttpClient getHttpClient()
        {
            if (_httpClient == null)
            {
                HttpClientHandler handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression && EnableCompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }

                _httpClient = new HttpClient(handler);
                _httpClient.BaseAddress = new Uri($"{URL}/");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("X-chkp-sid", SID);
            }

            return _httpClient;
        }
    }
}