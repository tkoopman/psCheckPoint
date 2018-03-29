using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-session">Get-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointSession</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSession")]
    [OutputType(typeof(Koopman.CheckPoint.SessionInfo))]
    public class GetCheckPointSession : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">Session unique identifier. If not provided the current logged in session UID will be used.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string UID { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            WriteObject(Session.FindSession(UID));
        }
    }
}