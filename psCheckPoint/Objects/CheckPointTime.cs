using Newtonsoft.Json;
using System;

namespace psCheckPoint.Objects
{
    public class CheckPointTime
    {
        /// <summary>
        /// <para type="description">Date and time represented in international ISO 8601 format.</para>
        /// </summary>
        [JsonProperty(PropertyName = "iso-8601", NullValueHandling = NullValueHandling.Ignore)]
        public string ISO8601 { get; set; }

        /// <summary>
        /// <para type="description">Number of milliseconds that have elapsed since 00:00:00, 1 January 1970.</para>
        /// </summary>
        [JsonProperty(PropertyName = "posix", NullValueHandling = NullValueHandling.Ignore)]
        public long Posix { get; set; }

        public DateTime asDateTime()
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return result.AddMilliseconds(Posix).ToLocalTime();
        }
    }
}