using Newtonsoft.Json;

namespace psCheckPoint.Objects._
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPoint_, Get-CheckPoint_ & Get-CheckPoint_s</para>
    /// <para type="description">_ object details.</para>
    /// </summary>
    public class CheckPoint_ : CheckPointObjectFull
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public CheckPointObject[] Groups { get; set; }
    }
}