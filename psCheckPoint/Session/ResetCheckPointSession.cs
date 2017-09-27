using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="discard">Reset-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log out of a session.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Close-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Reset, "CheckPointSession")]
    public class ResetCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "discard"; } }

        /// <summary>
        /// <para type="description">Reset none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public psCheckPoint.Objects.Session.CheckPointSession ResetSession { get; set; }

        /// <summary>
        /// <para type="description">Reset none active session UID</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string UID { get; set; }

        internal override string GetJSON()
        {
            // Check if we need to pass UID of session to reset
            // By not sending any UID API will reset current session.
            if (ResetSession != null)
            {
                UID = ResetSession.UID;
            }
            return base.GetJSON();
        }

        protected override void WriteRecordResponse(CheckPointMessage result)
        {
            WriteVerbose(result.Message);
        }
    }
}