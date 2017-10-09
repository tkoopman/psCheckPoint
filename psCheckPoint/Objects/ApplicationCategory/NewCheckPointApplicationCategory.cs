using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="add-application-site-category">New-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointApplicationCategory")]
    [OutputType(typeof(CheckPointApplicationCategory))]
    public class NewCheckPointApplicationCategory : NewCheckPointCmdlet<CheckPointApplicationCategory>
    {
        public override string Command { get { return "add-application-site-category"; } }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups
        {
            get { return _groups; }
            set { _groups = CreateArray(value); }
        }

        private string[] _groups;

        /// <summary>
        /// <para type="description">A description for the application.</para>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description { get; set; }
    }
}