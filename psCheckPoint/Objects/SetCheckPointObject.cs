using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
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
        protected string DetailsLevel { get; set; } = "standard";

        /// <summary>
        /// <para type="description">New name of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "new-name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string NewName { get; set; }

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
    }
}