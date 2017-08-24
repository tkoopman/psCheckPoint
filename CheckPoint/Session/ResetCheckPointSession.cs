using System.Management.Automation;

namespace CheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Log out of a sesison.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Close-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Reset, "CheckPointSession")]
    public class ResetCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        public override string Command { get { return "discard"; } }
    }
}