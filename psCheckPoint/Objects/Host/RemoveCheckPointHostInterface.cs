﻿using Newtonsoft.Json;
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
    /// <para type="synopsis">Remove host interface.</para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "CheckPointHostInterface")]
    [OutputType(typeof(CheckPointHost))]
    public class RemoveCheckPointHostInterface : CheckPointCmdlet<CheckPointHost>
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

        /// <summary>
        /// <para type="description">Interface name.</para>
        /// </summary>
        [Parameter(Position = 2, Mandatory = true)]
        public string Name { get; set; }

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
                ["remove"] = Name
            };
        }
    }
}