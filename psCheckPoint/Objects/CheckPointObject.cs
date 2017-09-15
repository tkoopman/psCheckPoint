using Newtonsoft.Json;
using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Objects.AddressRange;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.GroupWithExclusion;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.MulticastAddressRange;
using psCheckPoint.Objects.Network;
using psCheckPoint.Objects.ServiceGroup;
using psCheckPoint.Objects.ServiceTCP;
using psCheckPoint.Objects.ServiceUDP;
using psCheckPoint.Session;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base summary details of Check Point Objects</para>
    /// </summary>
    public class CheckPointObject : ICheckPointObjectSummary
    {
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
        public string Name { get; private set; }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 900)]
        public string UID { get; private set; }

        /// <summary>
        /// <para type="description">Type of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = -101)]
        public string Type { get; private set; }

        /// <summary>
        /// <para type="description">Information about the domain the object belongs to..</para>
        /// </summary>
        [JsonProperty(PropertyName = "domain", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Order = 1000)]
        public CheckPointDomain Domain { get; private set; }

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
        /// <param name="Session">Current session used to get full defails</param>
        /// <returns>Full details of object. If psCheckPoint doesn't implement the commands to get the full details of this object yet, returns this. If object not found then returns null.</returns>
        public CheckPointObject ToFullObj(CheckPointSession Session)
        {
            CheckPointObject r;
            switch (this.Type)
            {
                case "access-rule":
                    {
                        // Unable to get full object from CheckPointObject as also need to know Layer
                        // This means it must already be a full access-rule object
                        return this;
                    }
                case "address-range":
                    {
                        r = GetCheckPointObject<CheckPointAddressRange>(Session, "Get-CheckPointAddressRange", typeof(GetCheckPointAddressRange));
                        break;
                    }
                case "group":
                    {
                        r = GetCheckPointObject<CheckPointGroup>(Session, "Get-CheckPointGroup", typeof(GetCheckPointGroup));
                        break;
                    }
                case "group-with-exclusion":
                    {
                        r = GetCheckPointObject<CheckPointGroupWithExclusion>(Session, "Get-CheckPointGroupWithExclusion", typeof(GetCheckPointGroupWithExclusion));
                        break;
                    }
                case "host":
                    {
                        r = GetCheckPointObject<CheckPointHost>(Session, "Get-CheckPointHost", typeof(GetCheckPointHost));
                        break;
                    }
                case "multicast-address-range":
                    {
                        r = GetCheckPointObject<CheckPointMulticastAddressRange>(Session, "Get-CheckPointMulticastAddressRange", typeof(GetCheckPointMulticastAddressRange));
                        break;
                    }
                case "network":
                    {
                        r = GetCheckPointObject<CheckPointNetwork>(Session, "Get-CheckPointNetwork", typeof(GetCheckPointNetwork));
                        break;
                    }
                case "service-group":
                    {
                        r = GetCheckPointObject<CheckPointServiceGroup>(Session, "Get-CheckPointServiceGroup", typeof(GetCheckPointServiceGroup));
                        break;
                    }
                case "service-tcp":
                    {
                        r = GetCheckPointObject<CheckPointServiceTCP>(Session, "Get-CheckPointServiceTCP", typeof(GetCheckPointServiceTCP));
                        break;
                    }
                case "service-udp":
                    {
                        r = GetCheckPointObject<CheckPointServiceUDP>(Session, "Get-CheckPointServiceUDP", typeof(GetCheckPointServiceUDP));
                        break;
                    }
                default:
                    {
                        r = this;
                        break;
                    }
            }

            return r;
        }

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <typeparam name="T">Type object should be returned as.</typeparam>
        /// <param name="Session">Current session used to get full defails.</param>
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

        private T GetCheckPointObject<T>(CheckPointSession Session, string psCmdletName, Type psCmdlet) where T : CheckPointObject
        {
            if (this is T) { return (T)this; }

            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo(psCmdletName, psCmdlet));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", this.UID);

                Collection<T> results = PSI.Invoke<T>();
                return results.First();
            }
        }
    }
}