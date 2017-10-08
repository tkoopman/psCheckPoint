using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="run-script">Invoke-CheckPointScript</api>
    /// <summary>
    /// <para type="synopsis">Run script on gateways</para>
    /// <para type="description">Run a script on target gateways. After completing you can get any output from script by looking at the task details.</para>
    /// </summary>
    /// <example>
    ///   <code>$(Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'Show Configuration'" -Targets fwm-devbtpp001 | Wait-CheckPointTask).TaskDetails.ResponseMessage</code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Invoke, "CheckPointScript")]
    public class InvokeCheckPointScript : CheckPointCmdlet<CheckPointTaskIDs>
    {
        public override string Command { get { return "run-script"; } }

        /// <summary>
        /// <para type="description">Script Name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "script-name")]
        [Parameter(Mandatory = true)]
        public string ScriptName { get; set; }

        /// <summary>
        /// <para type="description">Script Body</para>
        /// </summary>
        [JsonProperty(PropertyName = "script")]
        [Parameter(ParameterSetName = "By Inline Script", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Script { get; set; }

        /// <summary>
        /// <para type="description">Load Script Body from File</para>
        /// </summary>
        [Parameter(ParameterSetName = "By Script File", Mandatory = true)]
        public string ScriptFile { get; set; }

        /// <summary>
        /// <para type="description">Script arguments.</para>
        /// </summary>
        [JsonProperty(PropertyName = "args")]
        [Parameter]
        public string Args { get; set; }

        /// <summary>
        /// <para type="description">On what targets to execute this command. Targets may be identified by their name, or object unique identifier.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public PSObject Targets { get; set; }

        [JsonProperty(PropertyName = "targets")]
        private string _targets;

        /// <summary>
        /// <para type="description">Comments string</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        [Parameter]
        public string Comments { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            SetInputIdentifier(Targets, "simple-gateway", out _targets, out _targets);

            if (ScriptFile != null)
            {
                Script = System.IO.File.ReadAllText(ScriptFile);
            }
        }

        protected override void WriteRecordResponse(CheckPointTaskIDs result)
        {
            foreach (CheckPointTaskID task in result)
            {
                WriteObject(task);
            }
        }
    }

    [JsonObject]
    public class CheckPointTaskIDs : IEnumerable<CheckPointTaskID>
    {
        [JsonConstructor]
        private CheckPointTaskIDs(List<CheckPointTaskID> tasks)
        {
            Tasks = tasks;
        }

        [JsonProperty(PropertyName = "tasks")]
        public List<CheckPointTaskID> Tasks { get; private set; }

        public IEnumerator<CheckPointTaskID> GetEnumerator()
        {
            return ((IEnumerable<CheckPointTaskID>)Tasks).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CheckPointTaskID>)Tasks).GetEnumerator();
        }
    }

    public class CheckPointTaskID
    {
        [JsonConstructor]
        private CheckPointTaskID(string target, string taskID)
        {
            Target = target;
            TaskID = taskID;
        }

        [JsonProperty(PropertyName = "target")]
        public string Target { get; private set; }

        [JsonProperty(PropertyName = "task-id")]
        public string TaskID { get; private set; }
    }
}