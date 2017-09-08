using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroup = Set-CheckPointGroup -Session $Session -Name Test1 -NewName Test2 -Tags TestTag</code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointServiceGroup")]
    [OutputType(typeof(CheckPointServiceGroup))]
    public class SetCheckPointServiceGroup : SetCheckPointObject<CheckPointServiceGroup>
    {
        public override string Command { get { return "set-service-group"; } }

        //TODO Add other member options for adding and removing
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