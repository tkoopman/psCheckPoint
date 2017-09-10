using Newtonsoft.Json;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <summary>
    /// <para type="description">Details of a Check Point Group with Exclusion</para>
    /// </summary>
    public class CheckPointGroupWithExclusion : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">Object which the group includes</para>
        /// </summary>
        [JsonProperty(PropertyName = "include", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Include { get; set; }

        /// <summary>
        /// <para type="description">Object which the group excludes</para>
        /// </summary>
        [JsonProperty(PropertyName = "except", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject Except { get; set; }
    }
}