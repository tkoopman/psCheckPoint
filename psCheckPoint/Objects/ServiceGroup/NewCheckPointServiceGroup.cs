using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="add-service-group">New-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointServiceGroup")]
    [OutputType(typeof(CheckPointServiceGroup))]
    public class NewCheckPointServiceGroup : NewCheckPointObject<CheckPointServiceGroup>
    {
        public override string Command { get { return "add-service-group"; } }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "members", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Members
        {
            get { return _members; }
            set { _members = CreateArray(value); }
        }

        private string[] _members;

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
    }
}