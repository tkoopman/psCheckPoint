using Newtonsoft.Json;

namespace psCheckPoint.Objects.Session
{
    /// <summary>
    /// <para type="description">Details of a Check Point Session</para>
    /// <para type="description">This session object does NOT include login details so cannot be used as Session parameter in other commands</para>
    /// </summary>
    public class CheckPointSession : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">The name of the application serving the web_api requests.</para>
        /// </summary>
        [JsonProperty(PropertyName = "application")]
        public string Application { get; set; }

        /// <summary>
        /// <para type="description">Number of pending changes.</para>
        /// </summary>
        [JsonProperty(PropertyName = "changes")]
        public int Changes { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "connection-mode")]
        public string ConnectionMode { get; set; }

        /// <summary>
        /// <para type="description">Session description.</para>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Administrator email.</para>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// <para type="description">True if the session is expired.</para>
        /// </summary>
        [JsonProperty(PropertyName = "expired-session")]
        public bool ExpiredSession { get; set; }

        /// <summary>
        /// <para type="description">True if the session is in work state.</para>
        /// </summary>
        [JsonProperty(PropertyName = "in-work")]
        public bool InWork { get; set; }

        /// <summary>
        /// <para type="description">IP address from which the session was initiated.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address")]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "last-login-time")]
        public CheckPointTime LastLoginTime { get; set; }

        /// <summary>
        /// <para type="description">Timestamp when user last accessed the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-logout-time")]
        public CheckPointTime LastLogoutTime { get; set; }

        /// <summary>
        /// <para type="description">Number of locked objects.</para>
        /// </summary>
        [JsonProperty(PropertyName = "locks")]
        public int Locks { get; set; }

        /// <summary>
        /// <para type="description">Administrator phone number.</para>
        /// </summary>
        [JsonProperty(PropertyName = "phone-number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// <para type="description">Timestamp when user published changes on the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "publish-time")]
        public CheckPointTime PublishTime { get; set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout")]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Session state.</para>
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// <para type="description">The name of the logged in user.</para>
        /// </summary>
        [JsonProperty(PropertyName = "user-name")]
        public string UserName { get; set; }
    }
}