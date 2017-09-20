using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-network">Get-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpNetwork = Get-CheckPointNetwork -Session $Session -Name Test1</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetwork")]
    [OutputType(typeof(CheckPointNetwork))]
    public class GetCheckPointNetwork : GetCheckPointObject<CheckPointNetwork>
    {
        public override string Command { get { return "show-network"; } }
    }
}