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
        public CheckPointSession(string SID, string APIServerVersion, string DiskSpaceMessage, CheckPointTime LastLoginWasAt, bool ReadOnly, int SessionTimeout, bool Standby, string UID, string URL)
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
        [JsonProperty(PropertyName = "sid")]
        public string SID { get; internal set; }

        /// <summary>
        /// <para type="description">API Server version.</para>
        /// </summary>
        [JsonProperty(PropertyName = "api-server-version")]
        public string APIServerVersion { get; internal set; }

        /// <summary>
        /// <para type="description">Information about the available disk space on the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "disk-space-message")]
        public string DiskSpaceMessage { get; internal set; }

        /// <summary>
        /// <para type="description">Timestamp when administrator last accessed the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-login-was-at")]
        public CheckPointTime LastLoginWasAt { get; internal set; }

        //TODO login-message

        /// <summary>
        /// <para type="description">True if this session is read only.</para>
        /// </summary>
        [JsonProperty(PropertyName = "read-only")]
        public bool ReadOnly { get; internal set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout")]
        public int SessionTimeout { get; internal set; }

        /// <summary>
        /// <para type="description">True if this management server is in the standby mode.</para>
        /// </summary>
        [JsonProperty(PropertyName = "standby")]
        public bool Standby { get; internal set; }

        /// <summary>
        /// <para type="description">Session object unique identifier. This identifier may be used in the discard API to discard changes that were made in this session, when administrator is working from another session, or in the 'switch-session' API.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string UID { get; internal set; }

        /// <summary>
        /// <para type="description">URL that was used to reach the API server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL { get; internal set; }

        internal bool EnableCompression { get; set; }

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