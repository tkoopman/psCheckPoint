using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPoint
{
    class CheckPointError
    {
        /// <summary>
        /// <para type="description">Error code.</para>
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// <para type="description">Operation status.</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
