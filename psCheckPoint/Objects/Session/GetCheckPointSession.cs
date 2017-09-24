using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-session">Get-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSession")]
    [OutputType(typeof(CheckPointSession))]
    public class GetCheckPointSession : CheckPointCmdlet<CheckPointSession>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-session"; } }

        /// <summary>
        /// <para type="description">Session unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string UID { get; set; }
    }
}