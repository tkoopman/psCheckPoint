using Newtonsoft.Json;
using System.Management.Automation;
using System.Threading.Tasks;
using static Koopman.CheckPoint.IA.IASession;

namespace psCheckPoint.IA
{
    /// <IA cmd="delete-identity">Remove-CheckPointIdentity</IA>
    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointIdentity")]
    [OutputType(typeof(DeleteIdentityResponse))]
    public class RemoveCheckPointIdentity : CheckPointIACmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// Deletes only associations created by the specified identity source.If no value is set for
        /// the client-type parameter, or if it is set to any, the gateway deletes all identities
        /// associated with the given IP(or IPs)
        /// </para>
        /// <para type="description">
        /// Note - When the client-type is set to vpn(remote access), the gateway deletes all the
        /// identities associated with the given IP(or IPs). This is because when you delete an
        /// identity associated with an office mode IP, this usually means that this office mode IP
        /// is no longer valid.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateSet("any", "captive-portal", "ida-agent", "vpn", "ad-query", "multihost-agent", "radius", "ida-api", "identity-collector", IgnoreCase = true)]
        public string ClientType { get; set; }

        /// <summary>
        /// <para type="description">Association IP. Required when you revoke a single IP.</para>
        /// </summary>
        [Parameter(ParameterSetName = "ip", Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">First IP in the range.Required when the revoke method is range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "range", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string IPAddressFirst { get; set; }

        /// <summary>
        /// <para type="description">Last IP in the range. Required when the revoke method is range.</para>
        /// </summary>
        [Parameter(ParameterSetName = "range", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string IPAddressLast { get; set; }

        /// <summary>
        /// <para type="description">Subnet. Required when the revoke method is mask.</para>
        /// </summary>
        [Parameter(ParameterSetName = "mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Subnet { get; set; }

        /// <summary>
        /// <para type="description">Subnet mask. Required when the revoke method is mask.</para>
        /// </summary>
        [Parameter(ParameterSetName = "mask", Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string SubnetMask { get; set; }

        #endregion Properties

        #region Methods

        protected override Task BeginProcessingAsync()
        {
            Session.StartDeleteBatch((r) => { WriteObject(r); }, maxBatchSize: BatchSize);
            return base.BeginProcessingAsync();
        }

        protected override Task ProcessRecordAsync()
        {
            switch (ParameterSetName)
            {
                case "ip":
                    Tasks.Add(Session.DeleteIdentity(IPAddress));
                    break;
            }
            return base.ProcessRecordAsync();
        }

        #endregion Methods
    }
}