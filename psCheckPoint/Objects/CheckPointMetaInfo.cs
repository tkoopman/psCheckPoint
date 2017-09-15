using Newtonsoft.Json;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Meta Information attached to Check Point Objects</para>
    /// </summary>
    public class CheckPointMetaInfo
    {
        [JsonConstructor]
        private CheckPointMetaInfo(CheckPointTime creationTime, string creator, string lastModifier, CheckPointTime lastModifyTime, string @lock, string validationState)
        {
            CreationTime = creationTime;
            Creator = creator;
            LastModifier = lastModifier;
            LastModifyTime = lastModifyTime;
            Lock = @lock;
            ValidationState = validationState;
        }

        /// <summary>
        /// <para type="description">Time object created</para>
        /// </summary>
        [JsonProperty(PropertyName = "creation-time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime CreationTime { get; private set; }

        /// <summary>
        /// <para type="description">Created by</para>
        /// </summary>
        [JsonProperty(PropertyName = "creator", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Creator { get; private set; }

        /// <summary>
        /// <para type="description">Last modified by</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modifier", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string LastModifier { get; private set; }

        /// <summary>
        /// <para type="description">Time last modified</para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modify-time", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointTime LastModifyTime { get; private set; }

        /// <summary>
        /// <para type="description">Object lock state. It's not allowed to edit objects locked by other session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "lock", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Lock { get; private set; }

        /// <summary>
        /// <para type="description">Any validation errors attached to the object</para>
        /// </summary>
        [JsonProperty(PropertyName = "validation-state", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string ValidationState { get; private set; }

        /// <summary>
        /// <para type="description">Convert Meta Info to a string</para>
        /// </summary>
        public override string ToString()
        {
            return $"{LastModifyTime.asDateTime().ToString()} by {LastModifier}";
        }
    }
}