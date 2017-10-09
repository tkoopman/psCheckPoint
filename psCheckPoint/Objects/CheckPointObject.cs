using Newtonsoft.Json;
using psCheckPoint.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base summary details of Check Point Objects</para>
    /// </summary>
    public class CheckPointObject : ICheckPointObjectSummary
    {
        private static readonly Dictionary<string, Object2FullDetails> _Object2FullDetails = new Dictionary<string, Object2FullDetails>
        {
            { "access-layer",              new Object2FullDetails( "Get-CheckPointAccessLayer",           typeof(AccessLayer.GetCheckPointAccessLayer) )                     },
            { "address-range",             new Object2FullDetails( "Get-CheckPointAddressRange",          typeof(AddressRange.GetCheckPointAddressRange) )                   },
            { "application-site",          new Object2FullDetails( "Get-CheckPointApplication",           typeof(Application.GetCheckPointApplication) )                     },
            { "application-site-category", new Object2FullDetails( "Get-CheckPointApplicationCategory",   typeof(ApplicationCategory.GetCheckPointApplicationCategory) )     },
            { "group",                     new Object2FullDetails( "Get-CheckPointGroup",                 typeof(Group.GetCheckPointGroup) )                                 },
            { "group-with-exclusion",      new Object2FullDetails( "Get-CheckPointGroupWithExclusion",    typeof(GroupWithExclusion.GetCheckPointGroupWithExclusion) )       },
            { "host",                      new Object2FullDetails( "Get-CheckPointHost",                  typeof(Host.GetCheckPointHost) )                                   },
            { "multicast-address-range",   new Object2FullDetails( "Get-CheckPointMulticastAddressRange", typeof(MulticastAddressRange.GetCheckPointMulticastAddressRange) ) },
            { "network",                   new Object2FullDetails( "Get-CheckPointNetwork",               typeof(Network.GetCheckPointNetwork) )                             },
            { "security-zone",             new Object2FullDetails( "Get-CheckPointSecurityZone",          typeof(SecurityZone.GetCheckPointSecurityZone) )                   },
            { "service-group",             new Object2FullDetails( "Get-CheckPointServiceGroup",          typeof(ServiceGroup.GetCheckPointServiceGroup) )                   },
            { "service-tcp",               new Object2FullDetails( "Get-CheckPointServiceTCP",            typeof(ServiceTCP.GetCheckPointServiceTCP) )                       },
            { "service-udp",               new Object2FullDetails( "Get-CheckPointServiceUDP",            typeof(ServiceUDP.GetCheckPointServiceUDP) )                       },
            { "simple-gateway",            new Object2FullDetails( "Get-CheckPointSimpleGateway",         typeof(SimpleGateway.GetCheckPointSimpleGateway) )                 }
        };

        /// <summary>
        /// JSON Constructor for Check Point Object Summary
        /// </summary>
        [JsonConstructor]
        protected CheckPointObject(string name, string uID, string type, CheckPointDomain domain)
        {
            Name = name;
            UID = uID;
            Type = type;
            Domain = domain;
        }

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = -100)]
        [DefaultValue("")]
        public string Name { get; private set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 900)]
        [DefaultValue("")]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">Type of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = -101)]
        [DefaultValue("")]
        public string Type { get; private set; }

        /// <summary>
        /// <para type="description">Information about the domain the object belongs to..</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 1000)]
        public CheckPointDomain Domain { get; private set; }

        /// <summary>
        /// Conditional Property Serialization for Domain
        /// </summary>
        /// <returns>true if Domain should be serialised.</returns>
        public bool ShouldSerializeDomain()
        {
            return !Domain.Equals(CheckPointDomain.DEFAULT);
        }

        /// <summary>
        /// <para type="description">Convert object to string. (Object name or UID if not Name)</para>
        /// </summary>
        public override string ToString()
        {
            return (String.IsNullOrWhiteSpace(Name)) ? UID : Name;
        }

        /// <summary>
        /// <para type="description">Returns true if object UIDs match</para>
        /// </summary>
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

        /// <summary>
        /// <para type="description">Returns Hashcode of object UID</para>
        /// </summary>
        public override int GetHashCode()
        {
            return this.UID.GetHashCode();
        }

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <param name="Session">Current session used to get full details</param>
        /// <returns>Full details of object. If psCheckPoint doesn't implement the commands to get the full details of this object yet, returns this. If object not found then returns null.</returns>
        public virtual CheckPointObject ToFullObj(CheckPointSession Session)
        {
            if (_Object2FullDetails.Keys.Contains(this.Type))
            {
                Object2FullDetails O2F = _Object2FullDetails[this.Type];

                return GetCheckPointObject(Session, O2F.psCmdletName, O2F.psCmdletType);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <typeparam name="T">Type object should be returned as.</typeparam>
        /// <param name="Session">Current session used to get full details.</param>
        /// <returns>Full details of object.</returns>
        /// <exception cref="InvalidCastException">If full object is not of type T.</exception>
        public T ToFullObj<T>(CheckPointSession Session) where T : CheckPointObject
        {
            CheckPointObject r = ToFullObj(Session);
            if (r is T)
            {
                return (r as T);
            }
            else
            {
                throw new InvalidCastException($"{r.GetType()} cannot be cast to {typeof(T).ToString()}");
            }
        }

        private CheckPointObject GetCheckPointObject(CheckPointSession Session, string psCmdletName, Type psCmdlet)
        {
            if (!(this is CheckPointObject)) { return this; }

            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo(psCmdletName, psCmdlet));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", this.UID);

                Collection<CheckPointObject> results = PSI.Invoke<CheckPointObject>();
                return results.First();
            }
        }
    }

    public class Object2FullDetails
    {
        public Object2FullDetails(string psCmdletName, Type psCmdletType)
        {
            this.psCmdletName = psCmdletName ?? throw new ArgumentNullException(nameof(psCmdletName));
            this.psCmdletType = psCmdletType ?? throw new ArgumentNullException(nameof(psCmdletType));
        }

        public string psCmdletName { get; private set; }
        public Type psCmdletType { get; private set; }
    }
}