using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-networks">Get-CheckPointNetworks</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpNetworks = Get-CheckPointNetworks -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetworks")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointNetworks : GetCheckPointObjects
    {
        public override string Command { get { return "show-networks"; } }
    }
}