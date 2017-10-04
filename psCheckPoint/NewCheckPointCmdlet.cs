﻿using Newtonsoft.Json;
using psCheckPoint.Objects;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for New-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class NewCheckPointCmdlet<T> : CheckPointColorCmdlet<T>
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags
        {
            get { return _tags; }
            set { _tags = CreateArray(value); }
        }

        private string[] _tags;

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ignore-warnings", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter IgnoreWarnings { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring errors. You won't be able to publish such a changes. If ignore-warnings flag was omitted - warnings will also be ignored.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ignore-errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter IgnoreErrors { get; set; }

        /// <summary>
        /// <para type="description">Return the updated object.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        protected override void WriteRecordResponse(T result)
        {
            if (result is CheckPointObject)
            {
                WriteVerbose($"{Command}: {(result as CheckPointObject).Name}");
            }
            if (PassThru.IsPresent) { WriteObject(result); }
        }
    }
}