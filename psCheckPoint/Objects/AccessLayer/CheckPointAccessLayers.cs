using Newtonsoft.Json;
using System.Collections.Generic;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <summary>
    /// <para type="description">Result from commands that return multiple objects.</para>
    /// </summary>
    public class CheckPointAccessLayers : CheckPointObjectsBase<CheckPointAccessLayer>
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "access-layers")]
        public List<CheckPointAccessLayer> AccessLayers { get { return _Objects; } private set { _Objects = value; } }
    }
}