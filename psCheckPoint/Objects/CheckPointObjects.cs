using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    public class CheckPointObjects<T>
    {
        /// <summary>
        /// <para type="description">From which element number the query was done.</para>
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public int From { get; set; }

        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "objects")]
        public T[] Objects { get; set; }

        /// <summary>
        /// <para type="description">To which element number the query was done.</para>
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public int To { get; set; }

        /// <summary>
        /// <para type="description">Total number of elements returned by the query.</para>
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}