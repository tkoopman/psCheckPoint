using Koopman.CheckPoint;
using System.Collections;
using System.Management.Automation;

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
        protected override void ProcessRecord()
        {
            ProcessObject(ResetSession);
        }

        private void ProcessObject(object obj)
        {
            if (obj == null) Session.Discard();
            else if (obj is string str) Session.Discard(str);
            else if (obj is SessionInfo o) Session.Discard(o.UID);
            else if (obj is PSObject pso) ProcessObject(pso.BaseObject);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    ProcessObject(eo);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", nameof(ResetSession));
        }

        #endregion Methods
    }
}