using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-networks">Get-CheckPointNetworks</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Get-CheckPointNetworks</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetworks")]
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointNetworks : GetCheckPointObjects
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-networks"; } }
    }
}