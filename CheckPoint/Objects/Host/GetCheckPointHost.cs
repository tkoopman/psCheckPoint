using Newtonsoft.Json;
using System.Management.Automation;

namespace CheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpHost = Get-CheckPointHost -Session $Session -Name Test1</code>
    /// </example>
    [JsonObject(MemberSerialization.OptIn)]
    [Cmdlet(VerbsCommon.Get, "CheckPointHost")]
    [OutputType(typeof(CheckPointHost))]
    public class GetCheckPointHost : GetCheckPointObject
    {
        protected override void ProcessRecord()
        {
            _ProcessRecord<CheckPointHost>("show-host");
        }
    }
}