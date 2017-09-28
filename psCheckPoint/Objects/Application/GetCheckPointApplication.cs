using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="show-application-site">Get-CheckPointApplication</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplication")]
    [OutputType(typeof(CheckPointApplication))]
    public class GetCheckPointApplication : GetCheckPointObject<CheckPointApplication>
    {
        public override string Command { get { return "show-application-site"; } }

        /// <summary>
        /// <para type="description">Object application identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "application-id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By Application ID", ValueFromPipelineByPropertyName = true)]
        public string ApplicationID { get; set; }
    }
}