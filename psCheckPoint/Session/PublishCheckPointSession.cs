using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="publish">Publish-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">
    /// All the changes done by this user will be seen by all users only after publish is called.
    /// </para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Publish-CheckPointSession
    /// </code>
    /// </example>
    [Cmdlet(VerbsData.Publish, "CheckPointSession")]
    public class PublishCheckPointSession : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Publish none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public Koopman.CheckPoint.SessionInfo PublishSession { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => Session.Publish(PublishSession?.UID);

        #endregion Methods
    }
}