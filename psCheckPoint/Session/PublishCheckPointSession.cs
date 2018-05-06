using System.Management.Automation;
using System.Threading.Tasks;

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
        protected override async Task ProcessRecordAsync() => await Session.Publish(PublishSession?.UID, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}