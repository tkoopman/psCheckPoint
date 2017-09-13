using Newtonsoft.Json;
using System.Collections;
using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpGroup = Set-CheckPointGroup -Session $Session -Name Test1 -NewName Test2 -Tags TestTag</code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointGroup")]
    [OutputType(typeof(CheckPointGroup))]
    public class SetCheckPointGroup : SetCheckPointObject<CheckPointGroup>
    {
        public override string Command { get { return "set-group"; } }

        //TODO Add other member options for adding and removing
        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "members", DefaultValueHandling = DefaultValueHandling.Ignore)]
        protected dynamic _members;

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Members
        {
            get { return _members; }
            set { _members = CreateArray(value); }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] AddMembers
        {
            get { return _members; }
            set
            {
                _members = new Hashtable();
                _members["add"] = CreateArray(value);
            }
        }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] RemoveMembers
        {
            get { return _members; }
            set
            {
                _members = new Hashtable();
                _members["remove"] = CreateArray(value);
            }
        }

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