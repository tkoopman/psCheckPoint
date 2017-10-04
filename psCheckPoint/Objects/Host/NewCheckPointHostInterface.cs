using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="set-host">Set-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Create new host interface.</para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "CheckPointHostInterface")]
    [OutputType(typeof(CheckPointHost))]
    public class NewCheckPointHostInterface : CheckPointCmdlet<CheckPointHost>
    {
        public override string Command { get { return "set-host"; } }

        /// <summary>
        /// <para type="description">Host object (Name, UID or Host Object)</para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public PSObject Host { get; set; }

        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string HostName { get; set; }

        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string HostUID { get; set; }

        [JsonProperty(PropertyName = "interfaces", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected Hashtable Interfaces { get; set; }

        private HostInterface _Interface = new HostInterface();

        /// <summary>
        /// <para type="description">Interface name.</para>
        /// </summary>
        [Parameter(Position = 2, Mandatory = true)]
        public string Name { get { return _Interface.Name; } set { _Interface.Name = value; } }

        /// <summary>
        /// <para type="description">IPv4 network address.</para>
        /// </summary>
        [Parameter]
        public string Subnet4 { get { return _Interface.Subnet4; } set { _Interface.Subnet4 = value; } }

        /// <summary>
        /// <para type="description">IPv6 network address.</para>
        /// </summary>
        [Parameter]
        public string Subnet6 { get { return _Interface.Subnet6; } set { _Interface.Subnet6 = value; } }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [Parameter]
        public int MaskLength4 { get { return _Interface.MaskLength4; } set { _Interface.MaskLength4 = value; } }

        /// <summary>
        /// <para type="description">IPv4 network mask length.</para>
        /// </summary>
        [Parameter]
        public int MaskLength6 { get { return _Interface.MaskLength6; } set { _Interface.MaskLength6 = value; } }

        /// <summary>
        /// <para type="description">Color of the object. Should be one of existing colors.</para>
        /// </summary>
        [Parameter]
        [Alias("Colour")]
        [ValidateColor]
        public string Color
        {
            get { return _Interface.Color; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _Interface.Color = null;
                }
                else
                {
                    _Interface.Color = value;
                }
            }
        }

        [OnSerializing]
        protected void OnSerializing(StreamingContext context)
        {
            if (Host.BaseObject is CheckPointObject)
            {
                HostUID = (Host.BaseObject as CheckPointObject).UID;
            }
            else if (Host.BaseObject is string)
            {
                string str = (Host.BaseObject as string);
                HostName = str;
            }
            else
            {
                throw new PSInvalidCastException("Host is invalid type.");
            }

            Interfaces = new Hashtable
            {
                ["add"] = _Interface
            };
        }

        public class HostInterface
        {
            [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal string Name { get; set; }

            [JsonProperty(PropertyName = "subnet4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal string Subnet4 { get; set; }

            [JsonProperty(PropertyName = "subnet6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal string Subnet6 { get; set; }

            [JsonProperty(PropertyName = "mask-length4", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal int MaskLength4 { get; set; }

            [JsonProperty(PropertyName = "mask-length6", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal int MaskLength6 { get; set; }

            [JsonProperty(PropertyName = "color", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
            internal string Color { get; set; }
        }
    }
}