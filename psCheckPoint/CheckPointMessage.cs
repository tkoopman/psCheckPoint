using Newtonsoft.Json;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Simple message result from command</para>
    /// </summary>
    public class CheckPointMessage
    {
        /// <summary>
        /// <para type="description">Message string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// <para type="description">Returns base message as string</para>
        /// </summary>
        public override string ToString()
        {
            return Message;
        }
    }
}