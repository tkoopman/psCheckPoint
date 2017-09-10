using Newtonsoft.Json;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Extra details of errors or warnings</para>
    /// </summary>
    public class CheckPointErrorDetail : CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Validation related to the current session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "current-session")]
        public bool CurrentSession { get; set; }
    }

    /// <summary>
    /// <para type="description">Result when commands return an error.</para>
    /// </summary>
    public class CheckPointError : CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Error code.</para>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// <para type="description">Validation warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "warnings")]
        public CheckPointErrorDetail[] Warnings { get; set; }

        /// <summary>
        /// <para type="description">Validation warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public CheckPointErrorDetail[] Errors { get; set; }

        /// <summary>
        /// <para type="description">Validation warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "blocking-errors")]
        public CheckPointErrorDetail[] BlockingErrors { get; set; }
    }
}