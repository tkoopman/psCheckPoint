using Newtonsoft.Json;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Simple message result from command</para>
    /// </summary>
    public class CheckPointMessage
    {
        [JsonConstructor]
        protected CheckPointMessage(string message)
        {
            Message = message;
        }

        /// <summary>
        /// <para type="description">Message string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "message", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        /// <summary>
        /// <para type="description">Returns base message as string</para>
        /// </summary>
        public override string ToString()
        {
            return Message;
        }
    }
}