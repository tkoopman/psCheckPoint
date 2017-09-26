using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-task">Get-CheckPointTask</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointTask")]
    public class GetCheckPointTask : CheckPointCmdlet<CheckPointTasks>
    {
        public override string Command { get { return "show-task"; } }

        /// <summary>
        /// <para type="description">Unique identifier of task</para>
        /// </summary>
        [JsonProperty(PropertyName = "task-id")]
        [Parameter(Mandatory = true)]
        public string TaskID { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "full";
    }
}