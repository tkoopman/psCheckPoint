using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="delete-access-layer">Remove-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>Remove-CheckPointAccessLayer -Name Network</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointAccessLayer")]
    public class RemoveCheckPointAccessLayer : RemoveCheckPointObject
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "delete-access-layer"; } }
    }
}