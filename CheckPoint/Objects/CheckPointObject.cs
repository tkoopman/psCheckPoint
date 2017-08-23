using Newtonsoft.Json;

namespace CheckPoint.Objects
{
    public class CheckPointObject
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Type of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// <para type="description">Information about the domain the object belongs to..</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain")]
        public CheckPointDomain Domain { get; set; }
    }
}