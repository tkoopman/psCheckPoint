using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Session
{
    /// <api cmd="logout">Close-CheckPointSession</api>
    /// <api cmd="continue-session-in-smartconsole">Close-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log out of a session.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Close-CheckPointSession
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Close, "CheckPointSession")]
    public class CloseCheckPointSession : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// The session will be continued next time your open SmartConsole. In case 'uid' is not
        /// provided, use current session. In order for the session to pass successfully to
        /// SmartConsole, make sure you don't have any other active GUI sessions.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter ContinueSessionInSmartconsole { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ContinueSessionInSmartconsole.IsPresent)
                await Session.ContinueSessionInSmartconsole(cancellationToken: CancelProcessToken);
            else
                await Session.Logout(cancellationToken: CancelProcessToken);

            Session.Dispose();

            if (IsPSSession)
                SessionState.PSVariable.Remove("CheckPointSession");
        }

        #endregion Methods
    }
}