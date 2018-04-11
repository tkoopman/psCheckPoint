using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="keepalive">Send-CheckPointKeepAlive</api>
    /// <summary>
    /// <para type="synopsis">Keep the session valid/alive.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Send-CheckPointKeepAlive
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommunications.Send, "CheckPointKeepAlive")]
    public class SendCheckPointKeepAlive : CheckPointCmdletBase
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => Session.SendKeepAlive();

        #endregion Methods
    }
}