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
    ///   <code>Close-CheckPointSession</code>
    /// </example>
    [Cmdlet(VerbsCommon.Close, "CheckPointSession")]
    public class CloseCheckPointSession : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">The session will be continued next time your open SmartConsole. In case 'uid' is not provided, use current session. In order for the session to pass successfully to SmartConsole, make sure you don't have any other active GUI sessions.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ContinueSessionInSmartconsole { get; set; }

        private bool IsPSSession = false;

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            IsPSSession = (Session == null);
            base.BeginProcessing();
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (ContinueSessionInSmartconsole.IsPresent)
                Session.ContinueSessionInSmartconsole();
            else
                Session.Logout();

            Session.Dispose();

            if (IsPSSession) { SessionState.PSVariable.Remove("CheckPointSession"); }
        }
    }
}