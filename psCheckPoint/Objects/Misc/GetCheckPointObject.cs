using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-object">Get-CheckPointObject</api>
    /// <summary>
    /// <para type="synopsis">Get object by UID.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointObject -UID "12345678-1234-1234-1234-123456789abc" | Get-CheckPointFullObject</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointObject")]
    [OutputType(typeof(CheckPointObject))]
    public class GetCheckPointObject : CheckPointCmdlet<CheckPointObject>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "show-object"; } }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";

        protected override CheckPointObject ProcessRecordResponse(string JSON)
        {
            // Debug Output Request
            WriteDebug($@"JSON Response
{JSON}");

            JObject results = JObject.Parse(JSON);
            CheckPointObject result = results["object"].ToObject<CheckPointObject>();

            return result;
        }
    }
}