using Newtonsoft.Json;

namespace psCheckPoint
{
    internal class CheckPointErrorDetail : CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Error code.</para>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public bool Code { get; set; }
    }

    internal class CheckPointError : CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Validation related to the current session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "current-session")]
        public string CurrentSession { get; set; }

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