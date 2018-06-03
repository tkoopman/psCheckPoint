using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-session">Get-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Session using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointSession
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSession")]
    [OutputType(typeof(Koopman.CheckPoint.SessionInfo))]
    public class GetCheckPointSession : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// Session unique identifier. If not provided the current logged in session UID will be used.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string UID { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindSession(UID, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}