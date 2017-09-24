using Newtonsoft.Json;
using psCheckPoint.Session;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="where-used">Get-CheckPointWhereUsed</api>
    /// <summary>
    /// <para type="synopsis">Searches for usage of the target object in other objects and rules.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>Get-CheckPointWhereUsed -Session $Session -Name http</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointWhereUsed", DefaultParameterSetName = "By Object")]
    [OutputType(typeof(CheckPointWhereUsed))]
    public class GetCheckPointWhereUsed : CheckPointCmdlet<CheckPointWhereUsed>
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "where-used"; } }

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By UID", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Force by UID. Used if pipelining in list of UIDs.</para>
        /// </summary>
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter(ParameterSetName = "By UID")]
        public SwitchParameter ByUID { get; set; }

        /// <summary>
        /// <para type="description">Object name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By Name", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Force by name. Used if pipelining in list of names.</para>
        /// </summary>
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter(ParameterSetName = "By Name")]
        public SwitchParameter ByName { get; set; }

        /// <summary>
        /// <para type="description">Check Point Object.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Object", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [PSDefaultValue(Value = null)]
        public CheckPointObject Object { set { UID = value.UID; } }

        /// <summary>
        /// <para type="description">Search for indirect usage.</para>
        /// </summary>
        [JsonProperty(PropertyName = "indirect", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter Indirect { get; set; }

        /// <summary>
        /// <para type="description">Maximum nesting level during indirect usage search.</para>
        /// </summary>
        [JsonProperty(PropertyName = "indirect-max-depth", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(5)]
        [Parameter]
        public int IndirectMaxDepth { get; set; } = 5;

        /// <summary>
        /// For running PowerShell Cmdlet from another C# class
        /// </summary>
        /// <param name="Session">Open Check Point session that will be used for running command</param>
        /// <param name="obj">Object to be run against.</param>
        /// <param name="indirect">Weather to include indirect uses.</param>
        /// <returns>Object contains results.</returns>
        public static CheckPointWhereUsed Run(CheckPointSession Session, CheckPointObject obj, bool indirect)
        {
            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo("Get-CheckPointWhereUsed", typeof(GetCheckPointWhereUsed)));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", obj.UID);
                if (indirect)
                {
                    PSI.AddParameter("Indirect");
                }

                Collection<CheckPointWhereUsed> results = PSI.Invoke<CheckPointWhereUsed>();
                return results.First();
            }
        }
    }
}