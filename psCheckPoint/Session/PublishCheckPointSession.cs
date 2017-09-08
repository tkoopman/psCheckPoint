using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">All the changes done by this user will be seen by all users only after publish is called.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Publish-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsData.Publish, "CheckPointSession")]
    public class PublishCheckPointSession : CheckPointCmdlet<CheckPointMessage>
    {
        public override string Command { get { return "publish"; } }

        /// <summary>
        /// <para type="description">Publish none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public psCheckPoint.Objects.Session.CheckPointSession PublishSession { get; set; }

        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string UID { get; set; }

        internal override string getJSON()
        {
            if (PublishSession != null)
            {
                UID = PublishSession.UID;
            }
            return base.getJSON();
        }
    }
}