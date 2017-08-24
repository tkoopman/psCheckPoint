using System.Management.Automation;

namespace CheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpHosts = Get-CheckPointHosts -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointHosts")]
    [OutputType(typeof(CheckPointObjects<CheckPointHost>))]
    public class GetCheckPointHosts : GetCheckPointObjects<CheckPointHost>
    {
        public override string Command { get { return "show-hosts"; } }
    }
}