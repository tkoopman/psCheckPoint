using Newtonsoft.Json;

namespace CheckPoint
{
    internal class CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Message string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}