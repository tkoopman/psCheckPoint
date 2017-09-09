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
            return (String.IsNullOrWhiteSpace(Name)) ? UID : Name;
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

        public CheckPointObject toFullObj(CheckPointSession Session)
        {
            CheckPointObject r = null;
            switch (this.Type)
            {
                case "access-rule":
                    {
                        r = GetCheckPointObject<CheckPointAccessRule>(Session, "Get-CheckPointAccessRule", typeof(GetCheckPointAccessRule));
                        break;
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

        public T toFullObj<T>(CheckPointSession Session) where T : CheckPointObject
        {
            CheckPointObject r = toFullObj(Session);
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