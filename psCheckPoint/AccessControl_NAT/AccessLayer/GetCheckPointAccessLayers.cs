using psCheckPoint.Objects;
using System.Management.Automation;

namespace psCheckPoint.AccessControl_NAT.AccessLayer
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessLayers")]
    [OutputType(typeof(CheckPointObjects<CheckPointAccessLayer>))]
    public class GetCheckPointAccessLayers : GetCheckPointObjects<CheckPointAccessLayer>
    {
        public override string Command { get { return "show-access-layers"; } }
    }
}