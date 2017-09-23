using Newtonsoft.Json;

namespace psCheckPoint.Objects.Session
{
    /// <summary>
    /// <para type="description">Details of a Check Point Session</para>
    /// <para type="description">This session object does NOT include login details so cannot be used as Session parameter in other commands</para>
    /// </summary>
    public class CheckPointSession : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointSession(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            string application, int changes, string connectionMode, string description, string email, bool expiredSession, bool inWork, string iPAddress, CheckPointTime lastLoginTime, CheckPointTime lastLogoutTime, int locks, string phoneNumber, CheckPointTime publishTime, int sessionTimeout, string state, string userName) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            Application = application;
            Changes = changes;
            ConnectionMode = connectionMode;
            Description = description;
            Email = email;
            ExpiredSession = expiredSession;
            InWork = inWork;
            IPAddress = iPAddress;
            LastLoginTime = lastLoginTime;
            LastLogoutTime = lastLogoutTime;
            Locks = locks;
            PhoneNumber = phoneNumber;
            PublishTime = publishTime;
            SessionTimeout = sessionTimeout;
            State = state;
            UserName = userName;
        }

        /// <summary>
        /// <para type="description">The name of the application serving the web_api requests.</para>
        /// </summary>
        [JsonProperty(PropertyName = "application", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Application { get; set; }

        /// <summary>
        /// <para type="description">Number of pending changes.</para>
        /// </summary>
        [JsonProperty(PropertyName = "changes", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Changes { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "connection-mode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string ConnectionMode { get; set; }

        /// <summary>
        /// <para type="description">Session description.</para>
        /// </summary>
        [JsonProperty(PropertyName = "description", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Administrator email.</para>
        /// </summary>
        [JsonProperty(PropertyName = "email", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// <para type="description">True if the session is expired.</para>
        /// </summary>
        [JsonProperty(PropertyName = "expired-session", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool ExpiredSession { get; set; }

        /// <summary>
        /// <para type="description">True if the session is in work state.</para>
        /// </summary>
        [JsonProperty(PropertyName = "in-work", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool InWork { get; set; }

        /// <summary>
        /// <para type="description">IP address from which the session was initiated.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ip-address", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "last-login-time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime LastLoginTime { get; set; }

        /// <summary>
        /// <para type="description">Timestamp when user last accessed the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-logout-time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime LastLogoutTime { get; set; }

        /// <summary>
        /// <para type="description">Number of locked objects.</para>
        /// </summary>
        [JsonProperty(PropertyName = "locks", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Locks { get; set; }

        /// <summary>
        /// <para type="description">Administrator phone number.</para>
        /// </summary>
        [JsonProperty(PropertyName = "phone-number", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// <para type="description">Timestamp when user published changes on the management server.</para>
        /// </summary>
        [JsonProperty(PropertyName = "publish-time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime PublishTime { get; set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds.</para>
        /// </summary>
        [JsonProperty(PropertyName = "session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">Session state.</para>
        /// </summary>
        [JsonProperty(PropertyName = "state", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        /// <summary>
        /// <para type="description">The name of the logged in user.</para>
        /// </summary>
        [JsonProperty(PropertyName = "user-name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }
    }
}