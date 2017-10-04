using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="show-access-layers">Get-CheckPointAccessLayers</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointAccessLayers</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessLayers")]
    [OutputType(typeof(CheckPointAccessLayers))]
    public class GetCheckPointAccessLayers : GetCheckPointObjectsBase<CheckPointAccessLayers>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-access-layers"; } }
    }
}