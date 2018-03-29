using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="discard">Reset-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log out of a session.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Reset-CheckPointSession</code>
    /// </example>
    [Cmdlet(VerbsCommon.Reset, "CheckPointSession")]
    public class ResetCheckPointSession : CheckPointCmdletBase
    {

        /// <summary>
        /// <para type="description">Reset none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public Koopman.CheckPoint.SessionInfo ResetSession { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            Session.Discard(ResetSession?.UID);
        }
    }
}