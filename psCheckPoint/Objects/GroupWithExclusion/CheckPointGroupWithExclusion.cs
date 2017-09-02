using Newtonsoft.Json;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    public class CheckPointGroupWithExclusion : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">Object which the group includes</para>
        /// </summary>
        [JsonProperty(PropertyName = "include")]
        public CheckPointObject Include { get; set; }

        /// <summary>
        /// <para type="description">Object which the group excludes</para>
        /// </summary>
        [JsonProperty(PropertyName = "except")]
        public CheckPointObject Except { get; set; }
    }
}