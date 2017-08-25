using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">All the changes done by this user will be seen by all users only after publish is called.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Publish-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsData.Publish, "CheckPointSession")]
    public class PublishCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        public override string Command { get { return "publish"; } }

        //TODO uid
    }
}