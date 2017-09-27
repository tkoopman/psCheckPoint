using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <api cmd="publish">Publish-CheckPointSession</api>
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
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public override string Command { get { return "publish"; } }

        /// <summary>
        /// <para type="description">Publish none active session</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public psCheckPoint.Objects.Session.CheckPointSession PublishSession { get; set; }

        /// <summary>
        /// <para type="description">Publish none active session UID</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        protected string UID { get; set; }

        internal override string GetJSON()
        {
            // Check if we need to pass UID of session to publish
            // By not sending any UID API will publish current session.
            if (PublishSession != null)
            {
                UID = PublishSession.UID;
            }
            return base.GetJSON();
        }

        protected override void WriteRecordResponse(CheckPointMessage result)
        {
            WriteVerbose(result.Message);
        }
    }
}