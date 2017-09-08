using Newtonsoft.Json;
using System;

namespace psCheckPoint.Objects
{
    public class CheckPointObject
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Type of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// <para type="description">Information about the domain the object belongs to..</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain", NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointDomain Domain { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            try
            {
                CheckPointObject OBJ = (CheckPointObject)obj;
                return this.UID.Equals(OBJ.UID);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}