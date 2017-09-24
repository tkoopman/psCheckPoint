using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.IA
{
    /// <IA cmd="delete-identity">Remove-CheckPointIdentity</IA>
    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Remove-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2</code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointIdentity")]
    [OutputType(typeof(RemoveIdentityResponse))]
    public class RemoveCheckPointIdentity : CheckPointIACmdlet
    {
        /// <summary>
        /// <para type="description">Association IP. Required when you revoke a single IP.</para>
        /// </summary>
        [Parameter(ParameterSetName = "ip", Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string IPAddress { get; set; }

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
        /// <para type="description">Deletes only associations created by the specified identity source.If no value is set for the client-type parameter, or if it is set to any, the gateway deletes all identities associated with the given IP(or IPs)</para>
        /// <para type="description">Note - When theclient-type is set to vpn(remote access), the gateway deletes all the identities associated with the given IP(or IPs). This is because when you delete an identity associated with an office mode IP, this usually means that this office mode IP is no longer valid.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateSet("any", "captive-portal", "ida-agent", "vpn", "ad-query", "multihost-agent", "radius", "ida-api", "identity-collector", IgnoreCase = true)]
        public string ClientType { get; set; }

        private Batch<RemoveIdentity, RemoveIdentityResponse> batch;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            batch = new Batch<RemoveIdentity, RemoveIdentityResponse>(Gateway, SharedSecret, "delete-identity");
        }

        protected override void ProcessRecord()
        {
            string revokeMethod = (IPAddress == null) ? ParameterSetName : null;
            batch.Requests.Add(new RemoveIdentity(IPAddress, revokeMethod, Subnet, SubnetMask, IPAddressFirst, IPAddressLast, ClientType));
            if (batch.Requests.Count >= BatchSize)
            {
                batch.Post(this);
                batch.Clear();
            }
        }

        protected override void EndProcessing()
        {
            if (batch.Requests.Count >= 1)
            {
                batch.Post(this);
            }

            batch.Dispose();
            batch = null;
        }
    }

    /// <summary>
    /// Stores identity to be removed ready for serilization to JSON request
    /// </summary>
    internal class RemoveIdentity
    {
        public RemoveIdentity(string iPAddress, string revokeMethod, string subnet, string subnetMask, string iPAddressFirst, string iPAddressLast, string clientType)
        {
            IPAddress = iPAddress;
            RevokeMethod = revokeMethod;
            Subnet = subnet;
            SubnetMask = subnetMask;
            IPAddressFirst = iPAddressFirst;
            IPAddressLast = iPAddressLast;
            ClientType = clientType;
        }

        [JsonProperty(PropertyName = "ip-address", NullValueHandling = NullValueHandling.Ignore)]
        public string IPAddress { get; set; }

        [JsonProperty(PropertyName = "revoke-method", NullValueHandling = NullValueHandling.Ignore)]
        public string RevokeMethod { get; set; }

        [JsonProperty(PropertyName = "subnet", NullValueHandling = NullValueHandling.Ignore)]
        public string Subnet { get; set; }

        [JsonProperty(PropertyName = "subnet-mask", NullValueHandling = NullValueHandling.Ignore)]
        public string SubnetMask { get; set; }

        [JsonProperty(PropertyName = "ip-address-first", NullValueHandling = NullValueHandling.Ignore)]
        public string IPAddressFirst { get; set; }

        [JsonProperty(PropertyName = "ip-address-last", NullValueHandling = NullValueHandling.Ignore)]
        public string IPAddressLast { get; set; }

        [JsonProperty(PropertyName = "client-type", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientType { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Response from Remove-CheckPointIdentity</para>
    /// <para type="description"></para>
    /// </summary>
    public class RemoveIdentityResponse
    {
        /// <summary>
        /// <para type="description">Deleted IPv4 association</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address")]
        public string IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">Deleted IPv6 association</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address")]
        public string IPv6Address { get; set; }

        /// <summary>
        /// <para type="description">Textual description of the command’s result</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// <para type="description">Number of deleted identities</para>
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public uint Count { get; set; }
    }
}