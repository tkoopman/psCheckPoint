using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Meta Information attached to Check Point Objects</para>
    /// </summary>
    public class CheckPointMetaInfo
    {
        /// <summary>
        /// <para type="description">Time object created</para>
        /// </summary>
        [JsonProperty(PropertyName = "creation-time", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime CreationTime { get; set; }

        /// <summary>
        /// <para type="description">Created by</para>
        /// </summary>
        [JsonProperty(PropertyName = "creator", NullValueHandling = NullValueHandling.Ignore)]
        public string Creator { get; set; }

        /// <summary>
        /// <para type="description">Last modified by</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modifier", NullValueHandling = NullValueHandling.Ignore)]
        public string LastModifier { get; set; }

        /// <summary>
        /// <para type="description">Time last modified</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modify-time", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime LastModifyTime { get; set; }

        /// <summary>
        /// <para type="description">Object lock state. It's not allowed to edit objects locked by other session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "lock", NullValueHandling = NullValueHandling.Ignore)]
        public string Lock { get; set; }

        /// <summary>
        /// <para type="description">Any validation errors attached to the object</para>
        /// </summary>
        [JsonProperty(PropertyName = "validation-state", NullValueHandling = NullValueHandling.Ignore)]
        public string ValidationState { get; set; }

        /// <summary>
        /// <para type="description">Convert Meta Info to a string</para>
        /// </summary>
        public override string ToString()
        {
            return $"Last modified by {LastModifier} on the {LastModifyTime.asDateTime().ToShortDateString()} at {LastModifyTime.asDateTime().ToShortTimeString()}";
        }
    }
}