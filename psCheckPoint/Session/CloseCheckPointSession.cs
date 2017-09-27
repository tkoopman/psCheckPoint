using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="logout">Close-CheckPointSession</api>
    /// <api cmd="continue-session-in-smartconsole">Close-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log out of a session.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Close-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Close, "CheckPointSession")]
    public class CloseCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        /// <summary>
        /// <para type="description">The session will be continued next time your open SmartConsole. In case 'uid' is not provided, use current session. In order for the session to pass successfully to SmartConsole, make sure you don't have any other active GUI sessions.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ContinueSessionInSmartconsole { get; set; }

        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return (ContinueSessionInSmartconsole.IsPresent) ? "continue-session-in-smartconsole" : "logout"; } }

        protected override void WriteRecordResponse(CheckPointMessage result)
        {
            WriteVerbose(result.Message);
        }
    }
}