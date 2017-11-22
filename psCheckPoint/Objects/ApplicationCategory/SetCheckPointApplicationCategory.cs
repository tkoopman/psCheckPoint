using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="set-application-site-category">Set-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointApplicationCategory")]
    [OutputType(typeof(CheckPointApplicationCategory))]
    public class SetCheckPointApplication : SetCheckPointObject<CheckPointApplicationCategory>
    {
        public override string Command { get { return "set-application-site-category"; } }

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
        }

        /// <summary>
        /// <para type="description">A description for the application.</para>
        /// </summary>
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description { get; set; }
    }
}