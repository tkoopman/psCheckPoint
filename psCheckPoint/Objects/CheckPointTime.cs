using Newtonsoft.Json;
using System;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Check Point Object's Time Fields</para>
    /// </summary>
    public class CheckPointTime
    {
        [JsonConstructor]
        private CheckPointTime(string iSO8601, long posix)
        {
            ISO8601 = iSO8601;
            Posix = posix;
        }

        /// <summary>
        /// <para type="description">Date and time represented in international ISO 8601 format.</para>
        /// </summary>
        [JsonProperty(PropertyName = "iso-8601", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string ISO8601 { get; set; }

        /// <summary>
        /// <para type="description">Number of milliseconds that have elapsed since 00:00:00, 1 January 1970.</para>
        /// </summary>
        [JsonProperty(PropertyName = "posix", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public long Posix { get; set; }

        /// <summary>
        /// <para type="description">Return as a standard c# DateTime object</para>
        /// </summary>
        public DateTime asDateTime()
        {
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return result.AddMilliseconds(Posix).ToLocalTime();
        }

        /// <summary>
        /// Returns as string
        /// </summary>
        /// <returns>String of date and time</returns>
        public override string ToString()
        {
            return this.asDateTime().ToString();
        }
    }
}