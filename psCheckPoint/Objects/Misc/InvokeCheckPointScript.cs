using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="run-script">Invoke-CheckPointScript</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
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
        [Parameter(ParameterSetName = "By Inline Script", Mandatory = true, ValueFromPipeline = true)]
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
        [JsonProperty(PropertyName = "targets")]
        [Parameter(Mandatory = true)]
        public string[] Targets { get; set; }

        /// <summary>
        /// <para type="description">Comments string</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments")]
        [Parameter]
        public string Comments { get; set; }

        internal override string GetJSON()
        {
            if (ScriptFile != null)
            {
                Script = System.IO.File.ReadAllText(ScriptFile);
            }
            return base.GetJSON();
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