using System.Management.Automation;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="delete-host">Remove-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointHost -Name Test1 -Verbose</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointHost")]
    public class RemoveCheckPointHost : RemoveCheckPointObject
    {
        /// <inheritdoc/>
        protected override void Remove(string value)
        {
            Session.DeleteHost(value, Ignore);
        }
    }
}