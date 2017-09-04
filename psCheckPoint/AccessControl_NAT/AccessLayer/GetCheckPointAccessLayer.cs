using psCheckPoint.Objects;
using System.Management.Automation;

namespace psCheckPoint.AccessControl_NAT.AccessLayer
{
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessLayer")]
    [OutputType(typeof(CheckPointAccessLayer))]
    public class GetCheckPointAccessLayer : GetCheckPointObject<CheckPointAccessLayer>
    {
        public override string Command { get { return "show-access-layer"; } }
    }
}