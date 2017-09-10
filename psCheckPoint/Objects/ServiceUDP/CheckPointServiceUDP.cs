using Newtonsoft.Json;
using psCheckPoint.Objects.Service;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <summary>
    /// <para type="description">Details of a Check Point UDP Service</para>
    /// </summary>
    public class CheckPointServiceUDP : CheckPointService
    {
        public override string ToString()
        {
            return $"{Name} (udp/{Port})";
        }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "accept-replies")]
        public bool AcceptReplies { get; set; }
    }
}