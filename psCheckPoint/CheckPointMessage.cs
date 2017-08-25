using Newtonsoft.Json;

namespace psCheckPoint
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