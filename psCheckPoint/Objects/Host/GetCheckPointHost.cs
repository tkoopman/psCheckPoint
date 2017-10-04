using System.Management.Automation;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="show-host">Get-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>$cpHost = Get-CheckPointHost -Name Test1</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointHost")]
    [OutputType(typeof(CheckPointHost))]
    public class GetCheckPointHost : GetCheckPointObject<CheckPointHost>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-host"; } }
    }
}