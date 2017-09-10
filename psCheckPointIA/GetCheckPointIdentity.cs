using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPointIA
{
    internal class GetIdentity
    {
        public GetIdentity(string iPAddress)
        {
            IPAddress = iPAddress;
        }

        [JsonProperty(PropertyName = "ip-address")]
        public string IPAddress { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Response from Get-CheckPointIdentity</para>
    /// <para type="description"></para>
    /// </summary>
    public class GetIdentityResponse
    {
        /// <summary>
        /// <para type="description">Queried IPv4 identity</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address")]
        public string IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">Queried IPv6 identity</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address")]
        public string IPv6Address { get; set; }

        /// <summary>
        /// <para type="description">Textual description of the command’s result</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// <para type="description">All user identities on this IP.</para>
        /// </summary>
        [JsonProperty(PropertyName = "users")]
        public GetIdentityResponseUser[] Users { get; set; }

        /// <summary>
        /// <para type="description">Computer name, if available</para>
        /// </summary>
        [JsonProperty(PropertyName = "machine")]
        public string Machine { get; set; }

        /// <summary>
        /// <para type="description">List of computer groups</para>
        /// </summary>
        [JsonProperty(PropertyName = "machine-groups")]
        public string[] MachineGroups { get; set; }

        /// <summary>
        /// <para type="description">List of all the access roles on this IP, for auditing and enforcement purposes.</para>
        /// </summary>
        [JsonProperty(PropertyName = "combined-roles")]
        public string[] CombinedRoles { get; set; }

        /// <summary>
        /// <para type="description">Machine session’s identity source, if the machine session is available.</para>
        /// </summary>
        [JsonProperty(PropertyName = "machine-identity-source")]
        public string MachineIdentitySource { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Response from Get-CheckPointIdentity Users parameter</para>
    /// <para type="description"></para>
    /// </summary>
    public class GetIdentityResponseUser
    {
        /// <summary>
        /// <para type="description">Users' full names (full name if available, falls back to user name if not)</para>
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        /// <summary>
        /// <para type="description">Array of groups</para>
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public string[] Groups { get; set; }

        /// <summary>
        /// <para type="description">Array of roles</para>
        /// </summary>
        [JsonProperty(PropertyName = "roles")]
        public string[] Roles { get; set; }

        /// <summary>
        /// <para type="description">Identity source</para>
        /// </summary>
        [JsonProperty(PropertyName = "identity-source")]
        public string IdentitySource { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointIdentity")]
    [OutputType(typeof(AddIdentityResponse))]
    public class GetCheckPointIdentity : CheckPointIACmdlet
    {
        /// <summary>
        /// <para type="description">Identity IP</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public string IPAddress { get; set; }

        private Batch<GetIdentity, GetIdentityResponse> batch;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            batch = new Batch<GetIdentity, GetIdentityResponse>(Gateway, SharedSecret, "show-identity");
        }

        protected override void ProcessRecord()
        {
            batch.Requests.Add(new GetIdentity(IPAddress));
            if (batch.Requests.Count >= BatchSize)
            {
                batch.Post(this);
                batch = new Batch<GetIdentity, GetIdentityResponse>(Gateway, SharedSecret, "show-identity");
            }
        }

        protected override void EndProcessing()
        {
            if (batch.Requests.Count >= 1)
            {
                batch.Post(this);
                batch = null;
            }
        }
    }
}