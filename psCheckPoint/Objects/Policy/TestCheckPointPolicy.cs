using Newtonsoft.Json;
using System.Collections.Generic;
using System.Management.Automation;

namespace psCheckPoint.Objects.Policy
{
    /// <api cmd="verify-policy">Test-CheckPointPolicy</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Test, "CheckPointPolicy")]
    public class TestCheckPointPolicy : CheckPointCmdlet<Dictionary<string, string>>
    {
        public override string Command { get { return "verify-policy"; } }

        /// <summary>
        /// <para type="description">The name of the Policy Package to be installed.</para>
        /// </summary>
        [JsonProperty(PropertyName = "policy-package")]
        [Parameter(Mandatory = true)]
        public string PolicyPackage { get; private set; }

        protected override void WriteRecordResponse(Dictionary<string, string> result)
        {
            WriteObject(result["task-id"]);
        }
    }
}