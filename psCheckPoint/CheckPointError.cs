using Newtonsoft.Json;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Extra details of errors or warnings</para>
    /// </summary>
    public class CheckPointErrorDetail : CheckPointMessage
    {
        /// <summary>
        /// JSON Constructor for Errors
        /// </summary>
        [JsonConstructor]
        private CheckPointErrorDetail(string message,
            bool currentSession) :
            base(message)
        {
            CurrentSession = currentSession;
        }

        /// <summary>
        /// <para type="description">Validation related to the current session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "current-session", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool CurrentSession { get; private set; }
    }

    /// <summary>
    /// <para type="description">Result when commands return an error.</para>
    /// </summary>
    public class CheckPointError : CheckPointMessage
    {
        /// <summary>
        /// Constructor for Check Point Error. Normally only called by JsonConvert.DeserializeObject when there is an error returned.
        /// </summary>
        [JsonConstructor]
        protected CheckPointError(string message,
            string code, CheckPointErrorDetail[] warnings, CheckPointErrorDetail[] errors, CheckPointErrorDetail[] blockingErrors) :
            base(message)
        {
            Code = code;
            Warnings = warnings;
            Errors = errors;
            BlockingErrors = blockingErrors;
        }

        /// <summary>
        /// <para type="description">Error code.</para>
        /// </summary>
        [JsonProperty(PropertyName = "code", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; private set; }

        /// <summary>
        /// <para type="description">Validation warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "warnings", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] Warnings { get; private set; }

        /// <summary>
        /// <para type="description">Validation errors.</para>
        /// </summary>
        [JsonProperty(PropertyName = "errors", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] Errors { get; private set; }

        /// <summary>
        /// <para type="description">Validation blocking-errors.</para>
        /// </summary>
        [JsonProperty(PropertyName = "blocking-errors", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] BlockingErrors { get; private set; }
    }
}