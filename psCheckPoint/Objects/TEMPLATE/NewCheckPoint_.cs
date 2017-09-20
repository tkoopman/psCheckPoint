using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <api cmd="add-_">New-CheckPoint_</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPoint_")]
    [OutputType(typeof(CheckPoint_))]
    public class NewCheckPoint_ : NewCheckPointObject<CheckPoint_>
    {
        public override string Command { get { return "add-_"; } }

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