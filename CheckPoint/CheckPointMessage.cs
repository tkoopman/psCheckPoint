using Newtonsoft.Json;

namespace CheckPoint
{
    public class CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Message string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}