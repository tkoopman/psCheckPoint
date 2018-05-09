using Newtonsoft.Json;
using System.Management.Automation;
using System.Threading.Tasks;
using static Koopman.CheckPoint.IA.IASession;

namespace psCheckPoint.IA
{
    /// <IA cmd="show-identity">Get-CheckPointIdentity</IA>
    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointIdentity")]
    [OutputType(typeof(ShowIdentityResponse))]
    public class GetCheckPointIdentity : CheckPointIACmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Identity IP</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string IPAddress { get; set; }

        #endregion Properties

        #region Methods

        protected override Task BeginProcessingAsync()
        {
            Session.StartShowBatch((r) => { WriteObject(r); }, maxBatchSize: BatchSize);
            return base.BeginProcessingAsync();
        }

        protected override Task ProcessRecordAsync()
        {
            Tasks.Add(Session.ShowIdentity(IPAddress));
            return base.ProcessRecordAsync();
        }

        #endregion Methods
    }
}