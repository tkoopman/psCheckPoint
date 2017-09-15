using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Details of a Check Point Domain</para>
    /// </summary>
    public class CheckPointDomain
    {
        [JsonConstructor]
        private CheckPointDomain(string name, string uID, string domainType)
        {
            Name = name;
            UID = uID;
            DomainType = domainType;
        }

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        [DefaultValue("SMC User")]
        public string Name { get; private set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        [DefaultValue("41e821a0-3720-11e3-aa6e-0800200c9fde")]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">Domain type.</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain-type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 3)]
        [DefaultValue("domain")]
        public string DomainType { get; private set; }

        internal static CheckPointDomain DEFAULT = new CheckPointDomain("SMC User", "41e821a0-3720-11e3-aa6e-0800200c9fde", "domain");

        /// <summary>
        /// <para type="description">Convert domain object to string. (Domain name)</para>
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// <para type="description">Returns true if object UIDs match</para>
        /// </summary>
        public override bool Equals(object obj)
        {
            try
            {
                CheckPointDomain OBJ = (CheckPointDomain)obj;
                return this.UID.Equals(OBJ.UID);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// <para type="description">Returns Hashcode of object UID</para>
        /// </summary>
        public override int GetHashCode()
        {
            return this.UID.GetHashCode();
        }
    }
}