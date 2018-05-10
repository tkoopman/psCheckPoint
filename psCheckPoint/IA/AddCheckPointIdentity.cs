using Newtonsoft.Json;
using System;
using System.Management.Automation;
using System.Threading.Tasks;
using Koopman.CheckPoint.IA;

namespace psCheckPoint.IA
{
    /// <IA cmd="add-identity">Add-CheckPointIdentity</IA>
    /// <summary>
    /// <para type="synopsis">Creates a new Identity Awareness association for a specified IP address.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Add-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -User "Test User" -Machine "Test Machine" -Roles "Test Role"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Add, "CheckPointIdentity")]
    [OutputType(typeof(AddIdentityResponse))]
    public class AddCheckPointIdentity : CheckPointIACmdlet
    {
        #region Fields

        private string[] _MachineGroups;

        private string[] _Roles;

        private string[] _UserGroups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Domain name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">Type of host device. For example: Apple iOS device.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string HostType { get; set; }

        /// <summary>
        /// <para type="description">Association IP. Supports either IPv4 or IPv6, but not both.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">Computer name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Machine { get; set; }

        /// <summary>
        /// <para type="description">
        /// List of groups to which the computer belongs(when Identity Awareness does not fetch
        /// computer groups).
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] MachineGroups
        {
            get => _MachineGroups;
            set => _MachineGroups = CreateArray(value);
        }

        /// <summary>
        /// <para type="description">Host operating system. For example: Windows 7.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string MachineOS { get; set; }

        /// <summary>
        /// <para type="description">
        /// Defines whether Identity Awareness calculates the identity’s access roles.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoCalculateRoles { get; set; }

        /// <summary>
        /// <para type="description">
        /// Defines whether Identity Awareness fetches the machine's groups from the user directories
        /// defined in SmartConsole.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoFetchMachineGroups { get; set; }

        /// <summary>
        /// <para type="description">
        /// Defines whether Identity Awareness fetches the user's groups from the user directories
        /// defined in SmartConsole.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoFetchUserGroups { get; set; }

        /// <summary>
        /// <para type="description">
        /// List of roles to assign to this identity (when Identity Awareness does not calculate roles).
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Roles
        {
            get => _Roles;
            set => _Roles = CreateArray(value);
        }

        /// <summary>
        /// <para type="description">Timeout (in seconds) for this Identity Awareness association.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [ValidateRange(300, 86400)]
        public int SessionTimeout { get; set; } = 43200;

        /// <summary>
        /// <para type="description">User name</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string User { get; set; }

        /// <summary>
        /// <para type="description">
        /// List of groups to which the user belongs (when Identity Awareness does not fetch user groups).
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] UserGroups
        {
            get => _UserGroups;
            set => _UserGroups = CreateArray(value);
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Begins the processing asynchronous.
        /// </summary>
        /// <returns></returns>
        protected override Task BeginProcessingAsync()
        {
            Session.StartAddBatch((r) => { WriteObject(r); }, maxBatchSize: BatchSize);
            return base.BeginProcessingAsync();
        }

        /// <summary>
        /// Processes the record asynchronous.
        /// </summary>
        /// <returns></returns>
        protected override Task ProcessRecordAsync()
        {
            Tasks.Add(Session.AddIdentity(IPAddress, User, Machine, Domain, SessionTimeout, !NoFetchUserGroups.IsPresent, !NoFetchMachineGroups.IsPresent, !NoCalculateRoles.IsPresent, UserGroups, MachineGroups, Roles, MachineOS, HostType));
            return base.ProcessRecordAsync();
        }

        private static string[] CreateArray(string[] values)
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

        #endregion Methods
    }
}