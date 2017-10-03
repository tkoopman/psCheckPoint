using Newtonsoft.Json;
using System.Collections.Generic;

namespace psCheckPoint.Objects.Session
{
    /// <summary>
    /// <para type="description">Result from commands that return multiple objects.</para>
    /// </summary>
    public class CheckPointSessions : CheckPointObjectsBase<CheckPointSession>
    {
        /// <summary>
        /// <para type="description">How much details are returned depends on the details-level field of the request. This table shows the level of detail shown when details-level is set to standard.</para>
        /// </summary>
        [JsonProperty(PropertyName = "objects", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<CheckPointSession> Sessions { get { return _Objects; } set { _Objects = value; } }
    }
}