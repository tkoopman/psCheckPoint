using Newtonsoft.Json;
using System.Management.Automation;

namespace CheckPoint.Objects.Host
{
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpHosts = Get-CheckPointHosts -Session $Session</code>
    /// </example>
    [JsonObject(MemberSerialization.OptIn)]
    [Cmdlet(VerbsCommon.Get, "CheckPointHosts")]
    [OutputType(typeof(CheckPointObjects<CheckPointHost>))]
    public class GetCheckPointHosts : GetCheckPointObjects
    {
        protected override void ProcessRecord()
        {
            _ProcessRecord<CheckPointHost>("show-hosts");
        }
    }
}