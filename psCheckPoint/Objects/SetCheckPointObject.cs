using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Set-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class SetCheckPointObject<T> : CheckPointColorCmdlet<T>
    {
        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By UID", ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Object name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "By Name", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level")]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";

        /// <summary>
        /// <para type="description">New name of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "new-name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string NewName { get; set; }

        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private dynamic _tags;

        /// <summary>
        /// <para type="description">Action to take with tags.</para>
        /// </summary>
        [Parameter]
        public MembershipActions TagAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

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

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            OnSerializing();
        }

        /// <summary>
        /// <para type="description">Called when object is being serialized. Used for processing Group Actions.</para>
        /// </summary>
        protected virtual void OnSerializing()
        {
            _tags = ProcessGroupAction(TagAction, Tags);
        }
    }
}