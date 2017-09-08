using Newtonsoft.Json;

namespace psCheckPoint.Objects.Group
{
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    public class CheckPointGroup : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Groups { get; set; }

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID. How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "members", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointObject[] Members { get; set; }
    }
}