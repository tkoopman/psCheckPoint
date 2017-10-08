using System.Management.Automation;

namespace psCheckPoint.Objects.SimpleGateway
{
    /// <api cmd="show-simple-gateways">Get-CheckPointSimpleGateways</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSimpleGateways")]
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointSimpleGateways : GetCheckPointObjects
    {
        public override string Command { get { return "show-simple-gateways"; } }
    }
}