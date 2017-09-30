using Newtonsoft.Json;
using System;
using System.Management.Automation;

namespace psCheckPoint.IA
{
    /// <IA cmd="add-identity">Add-CheckPointIdentity</IA>
    /// <summary>
    /// <para type="synopsis">Creates a new Identity Awareness association for a specified IP address.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Add-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -User "Test User" -Machine "Test Machine" -Roles "Test Role"</code>
    /// </example>
    [Cmdlet(VerbsCommon.Add, "CheckPointIdentity")]
    [OutputType(typeof(AddIdentityResponse))]
    public class AddCheckPointIdentity : CheckPointIACmdlet
    {
        /// <summary>
        /// <para type="description">Association IP. Supports either IPv4 or IPv6, but not both.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">User name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string User { get; set; }

        /// <summary>
        /// <para type="description">Computer name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Machine { get; set; }

        /// <summary>
        /// <para type="description">Domain name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">Timeout (in seconds) for this Identity Awareness association.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateRange(300, 86400)]
        public int SessionTimeout { get; set; } = 43200;

        /// <summary>
        /// <para type="description">Defines whether Identity Awareness fetches the user's groups from the user directories defined in SmartConsole.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoFetchUserGroups { get; set; }

        /// <summary>
        /// <para type="description">Defines whether Identity Awareness fetches the machine's groups from the user directories defined in SmartConsole.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoFetchMachineGroups { get; set; }

        /// <summary>
        /// <para type="description">List of groups to which the user belongs (when Identity Awareness does not fetch user groups).</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] UserGroups
        {
            get { return _UserGroups; }
            set { _UserGroups = CreateArray(value); }
        }

        private string[] _UserGroups;

        /// <summary>
        /// <para type="description">List of groups to which the computer belongs(when Identity Awareness does not fetch computer groups).</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] MachineGroups
        {
            get { return _MachineGroups; }
            set { _MachineGroups = CreateArray(value); }
        }

        private string[] _MachineGroups;

        /// <summary>
        /// <para type="description">Defines whether Identity Awareness calculates the identity’s access roles.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoCalculateRoles { get; set; }

        /// <summary>
        /// <para type="description">List of roles to assign to this identity (when Identity Awareness does not calculate roles).</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Roles
        {
            get { return _Roles; }
            set { _Roles = CreateArray(value); }
        }

        private string[] _Roles;

        /// <summary>
        /// <para type="description">Host operating system. For example: Windows 7.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string MachineOS { get; set; }

        /// <summary>
        /// <para type="description">Type of host device. For example: Apple iOS device.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string HostType { get; set; }

        private Batch<Identity, AddIdentityResponse> batch;

        /// <summary>
        /// Provides a one-time, preprocessing functionality for the cmdlet.
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            batch = new Batch<Identity, AddIdentityResponse>(Gateway, SharedSecret, "add-identity", NoCertificateValidation.IsPresent);
        }

        /// <summary>
        /// Provides a record-by-record processing functionality for the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            batch.Requests.Add(new Identity(IPAddress, User, Machine, Domain, SessionTimeout, NoFetchUserGroups, NoFetchMachineGroups, UserGroups, MachineGroups, NoCalculateRoles, Roles, MachineOS, HostType));
            if (batch.Requests.Count >= BatchSize)
            {
                batch.Post(this);
                batch.Clear();
            }
        }

        /// <summary>
        /// Provides a one-time, post-processing functionality for the cmdlet.
        /// </summary>
        protected override void EndProcessing()
        {
            if (batch.Requests.Count >= 1)
            {
                batch.Post(this);
            }

            batch.Dispose();
            batch = null;
        }

        private static string[] CreateArray(String[] values)
        {
            if (values == null)
            {
                return null;
            }
            else
            {
                if (values.Length == 1)
                {
                    string value = values[0];
                    values = value.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                }

                return values;
            }
        }
    }

    /// <summary>
    /// Stores identity to be sent ready for serialization to JSON request.
    /// </summary>
    internal class Identity
    {
        public Identity(string iPAddress, string user, string machine, string domain, int sessionTimeout, SwitchParameter noFetchUserGroups, SwitchParameter noFetchMachineGroups, string[] userGroups, string[] machineGroups, SwitchParameter noCalculateRoles, string[] roles, string machineOS, string hostType)
        {
            IPAddress = iPAddress;
            User = user;
            Machine = machine;
            Domain = domain;
            SessionTimeout = sessionTimeout;
            NoFetchUserGroups = noFetchUserGroups;
            NoFetchMachineGroups = noFetchMachineGroups;
            UserGroups = userGroups;
            MachineGroups = machineGroups;
            NoCalculateRoles = noCalculateRoles;
            Roles = roles;
            MachineOS = machineOS;
            HostType = hostType;
        }

        [JsonProperty(PropertyName = "ip-address")]
        public string IPAddress { get; set; }

        [JsonProperty(PropertyName = "user", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string User { get; set; }

        [JsonProperty(PropertyName = "machine", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Machine { get; set; }

        [JsonProperty(PropertyName = "domain", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Domain { get; set; }

        [JsonProperty(PropertyName = "session-timeout", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SessionTimeout { get; set; } = 43200;

        [JsonProperty(PropertyName = "fetch-user-groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        public SwitchParameter NoFetchUserGroups { get; set; }

        [JsonProperty(PropertyName = "fetch-machine-groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        public SwitchParameter NoFetchMachineGroups { get; set; }

        [JsonProperty(PropertyName = "user-groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] UserGroups { get; set; }

        [JsonProperty(PropertyName = "machine-groups", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] MachineGroups { get; set; }

        [JsonProperty(PropertyName = "calculate-roles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        public SwitchParameter NoCalculateRoles { get; set; }

        [JsonProperty(PropertyName = "roles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Roles { get; set; }

        [JsonProperty(PropertyName = "machine-os", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MachineOS { get; set; }

        [JsonProperty(PropertyName = "host-type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string HostType { get; set; }
    }

    /// <summary>
    /// <para type="synopsis">Response from Add-CheckPointIdentity</para>
    /// <para type="description"></para>
    /// </summary>
    public class AddIdentityResponse
    {
        /// <summary>
        /// <para type="description">Created IPv4 identity</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv4-address")]
        public string IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">Created IPv6 identity</para>
        /// </summary>
        [JsonProperty(PropertyName = "ipv6-address")]
        public string IPv6Address { get; set; }

        /// <summary>
        /// <para type="description">Textual description of the command’s result</para>
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}