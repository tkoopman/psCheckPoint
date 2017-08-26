using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Objects
{
    public class CheckPointMetaInfo
    {
        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "creation-time")]
        public CheckPointTime CreationTime { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "creator")]
        public string Creator { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modifier")]
        public string LastModifier { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "last-modify-time")]
        public CheckPointTime LastModifyTime { get; set; }

        /// <summary>
        /// <para type="description">Object lock state. It's not allowed to edit objects locked by other session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "lock")]
        public string Lock { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [JsonProperty(PropertyName = "validation-state")]
        public string ValidationState { get; set; }
    }
}