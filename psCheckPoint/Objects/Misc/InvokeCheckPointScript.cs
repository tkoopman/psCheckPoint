using Koopman.CheckPoint;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="run-script">Invoke-CheckPointScript</api>
    /// <summary>
    /// <para type="synopsis">Run script on gateways</para>
    /// <para type="description">
    /// Run a script on target gateways. After completing you can get any output from script by
    /// looking at the task details.
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// $(Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'Show Configuration'" -Targets mgmt | Wait-CheckPointTask).TaskDetails.ResponseMessage
    /// </code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Invoke, "CheckPointScript")]
    public class InvokeCheckPointScript : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Script arguments.</para>
        /// </summary>
        [Parameter]
        public string Args { get; set; }

        /// <summary>
        /// <para type="description">Comments string</para>
        /// </summary>
        [Parameter]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">Script Body</para>
        /// </summary>
        [Parameter(ParameterSetName = "By Inline Script", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Script { get; set; }

        /// <summary>
        /// <para type="description">Load Script Body from File</para>
        /// </summary>
        [Parameter(ParameterSetName = "By Script File", Mandatory = true)]
        public string ScriptFile { get; set; }

        /// <summary>
        /// <para type="description">Script Name.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string ScriptName { get; set; }

        /// <summary>
        /// <para type="description">
        /// On what targets to execute this command. Targets may be identified by their name, or
        /// object unique identifier.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public PSObject Targets { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var targets = new List<string>();
            GetTargets(Targets, targets);

            if (ScriptFile != null)
                Script = File.ReadAllText(ScriptFile);

            WriteObject(Session.RunScript(ScriptName, Script, Args, targets.ToArray(), Comments));
        }

        private void GetTargets(object obj, List<string> output)
        {
            if (obj is string str) output.Add(str);
            else if (obj is IObjectSummary o) output.Add(o.GetIdentifier());
            else if (obj is PSObject pso) GetTargets(pso.BaseObject, output);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    GetTargets(eo, output);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", nameof(Targets));
        }

        #endregion Methods
    }
}