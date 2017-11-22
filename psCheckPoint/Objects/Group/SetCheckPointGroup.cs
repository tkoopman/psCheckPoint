using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="set-group">Set-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Set-CheckPointGroup -Name Test1 -NewName Test2 -Tags TestTag</code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointGroup")]
    [OutputType(typeof(CheckPointGroup))]
    public class SetCheckPointGroup : SetCheckPointObject<CheckPointGroup>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "set-group"; } }

        [JsonProperty(PropertyName = "members", NullValueHandling = NullValueHandling.Ignore)]
        private dynamic _members;

        /// <summary>
        /// <para type="description">Action to take with members.</para>
        /// </summary>
        [Parameter]
        public MembershipActions MemberAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID.</para>
        /// <para type="description">Members listed will be either Added, Removed or replace the current list of members based on MemberAction parameter.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Members { get; set; }

        [JsonProperty(PropertyName = "groups", NullValueHandling = NullValueHandling.Ignore)]
        private dynamic _groups;

        /// <summary>
        /// <para type="description">Action to take with groups.</para>
        /// </summary>
        [Parameter]
        public MembershipActions GroupAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// <para type="description">Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get; set; }

        /// <summary>
        /// <para type="description">Called when object is being serialized. Used for processing Group Actions.</para>
        /// </summary>
        protected override void OnSerializing()
        {
            base.OnSerializing();
            _groups = ProcessGroupAction(GroupAction, Groups);
            _members = ProcessGroupAction(MemberAction, Members);
        }
    }
}