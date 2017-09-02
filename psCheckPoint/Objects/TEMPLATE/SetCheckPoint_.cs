using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPoint_")]
    [OutputType(typeof(CheckPoint_))]
    public class SetCheckPoint_ : SetCheckPointObject<CheckPoint_>
    {
        public override string Command { get { return "set-_"; } }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups
        {
            get { return _groups; }
            set { _groups = CreateArray(value); }
        }

        private string[] _groups;
    }
}