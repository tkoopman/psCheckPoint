using Newtonsoft.Json;
using System.Management.Automation;

namespace CheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointHost -Session $Session -Name Test1 -Verbose</code>
    /// </example>
    [JsonObject(MemberSerialization.OptIn)]
    [Cmdlet(VerbsCommon.Remove, "CheckPointHost")]
    public class RemoveCheckPointHost : RemoveCheckPointObject
    {
        protected override void ProcessRecord()
        {
            _ProcessRecord("delete-host");
        }
    }
}