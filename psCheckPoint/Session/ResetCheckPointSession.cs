using Koopman.CheckPoint;
using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Session
{
    /// <api cmd="discard">Reset-CheckPointSession</api>
    /// <summary>
    /// <para type="synopsis">Log out of a session.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Reset-CheckPointSession
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Reset, "CheckPointSession")]
    public class ResetCheckPointSession : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Reset none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public PSObject ResetSession { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task ProcessRecordAsync() => ProcessObject(ResetSession);

        private async Task ProcessObject(object obj)
        {
            CancelProcessToken.ThrowIfCancellationRequested();
            if (obj == null) await Session.Discard();
            else if (obj is string str) await Session.Discard(str, cancellationToken: CancelProcessToken);
            else if (obj is SessionInfo o) await Session.Discard(o.UID, cancellationToken: CancelProcessToken);
            else if (obj is PSObject pso) await ProcessObject(pso.BaseObject);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    await ProcessObject(eo);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", nameof(ResetSession));
        }

        #endregion Methods
    }
}