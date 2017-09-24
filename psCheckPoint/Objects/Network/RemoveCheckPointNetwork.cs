using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="delete-network">Remove-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointNetwork -Session $Session -Name Test1 -Verbose</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointNetwork")]
    public class RemoveCheckPointNetwork : RemoveCheckPointObject<CheckPointMessage>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "delete-network"; } }
    }
}