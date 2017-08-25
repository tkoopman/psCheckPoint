using Newtonsoft.Json;

namespace psCheckPoint.Session
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

        //TODO last-login-was-at

        //TODO login-message

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
}