using System;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="login">Open-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log in to the server with user name and password.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Open-CheckPointSession -ManagementServer 192.168.1.1
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// $Session = Open-CheckPointSession -ManagementServer 192.168.1.1 -PassThru
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Open, "CheckPointSession")]
    [OutputType(typeof(Koopman.CheckPoint.Session))]
    public class OpenCheckPointSession : PSCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The new session would continue where the last session was stopped.</para>
        /// <para type="description">
        /// This option is available when the administrator has only one session that can be continued.
        /// </para>
        /// <para type="description">If there is more than one session, see 'switch-session' API.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ContinueLastSession { get; set; }

        /// <summary>
        /// <para type="description">
        /// PSCredential containing User name and Password. If not provided you will be prompted.
        /// </para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public PSCredential Credentials { get; set; }

        /// <summary>
        /// <para type="description">
        /// Use domain to login to specific domain. Domain can be identified by name or UID.
        /// </para>
        /// </summary>
        [Parameter]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">
        /// Login to the last published session. Such login is done with the Read Only permissions.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter EnterLastPublishedSession { get; set; }

        /// <summary>
        /// <para type="description">Port Web API running on. Default: 443</para>
        /// </summary>
        [Parameter]
        public int ManagementPort { get; set; } = 443;

        /// <summary>
        /// <para type="description">IP or Hostname of the Check point Management Server</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string ManagementServer { get; set; }

        /// <summary>
        /// <para type="description">Do NOT verify server's certificate.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoCertificateValidation { get; set; }

        /// <summary>
        /// <para type="description">Return the session and do not store it for automatic use.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">
        /// Login with Read Only permissions. This parameter is not considered in case
        /// continue-last-session is true.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter ReadOnly { get; set; }

        /// <summary>
        /// <para type="description">Session comments.</para>
        /// </summary>
        [Parameter]
        public string SessionComments { get; set; }

        /// <summary>
        /// <para type="description">Session description.</para>
        /// </summary>
        [Parameter]
        public string SessionDescription { get; set; }

        /// <summary>
        /// <para type="description">Session unique name.</para>
        /// </summary>
        [Parameter]
        public string SessionName { get; set; }

        /// <summary>
        /// <para type="description">Session expiration timeout in seconds. Default 600 seconds.</para>
        /// </summary>
        [Parameter]
        public int SessionTimeout { get; set; } = 600;

        private string Password { get; set; }
        private string User { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            try
            {
                Koopman.CheckPoint.Session session = new Koopman.CheckPoint.Session(
                    managementServer: ManagementServer,
                    domain: Domain,
                    userName: Credentials.GetNetworkCredential().UserName,
                    password: Credentials.GetNetworkCredential().Password,
                    port: ManagementPort,
                    readOnly: ReadOnly.IsPresent,
                    certificateValidation: !NoCertificateValidation.IsPresent,
                    sessionName: SessionName,
                    comments: SessionComments,
                    description: SessionDescription,
                    timeout: SessionTimeout,
                    continueLastSession: ContinueLastSession.IsPresent,
                    enterLastPublishedSession: EnterLastPublishedSession.IsPresent
                    );
                if (PassThru.IsPresent)
                    WriteObject(session);
                else
                    SessionState.PSVariable.Set("CheckPointSession", session);
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                this.WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }

        #endregion Methods
    }
}