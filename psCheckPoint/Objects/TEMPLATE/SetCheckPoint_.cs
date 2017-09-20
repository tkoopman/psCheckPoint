using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects._
{
    /// <api cmd="set-_">Set-CheckPoint_</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPoint_")]
    [OutputType(typeof(CheckPoint_))]
    public class SetCheckPoint_ : SetCheckPointObject<CheckPoint_>
    {
        public override string Command { get { return "set-_"; } }

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

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            _groups = ProcessGroupAction(GroupAction, Groups);
        }
    }
}