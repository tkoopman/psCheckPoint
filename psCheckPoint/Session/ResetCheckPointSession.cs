using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Log out of a sesison.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Close-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Reset, "CheckPointSession")]
    public class ResetCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        public override string Command { get { return "discard"; } }

        /// <summary>
        /// <para type="description">Reset none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public psCheckPoint.Objects.Session.CheckPointSession ResetSession { get; set; }

        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string UID { get; set; }

        internal override string getJSON()
        {
            if (ResetSession != null)
            {
                UID = ResetSession.UID;
            }
            return base.getJSON();
        }
    }
}