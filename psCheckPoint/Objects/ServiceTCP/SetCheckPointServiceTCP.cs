using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="set-service-tcp">Set-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Set, "CheckPointServiceTCP")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceTCP))]
    public class SetCheckPointServiceTCP : SetCheckPointCmdlet
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Action to take with groups.</para>
        /// </summary>
        [Parameter]
        public MembershipActions GroupAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// <para type="description">
        /// Groups listed will be either Added, Removed or replace the current list of group
        /// membership based on GroupAction parameter.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get => _groups; set => _groups = CreateArray(value); }

        /// <summary>
        /// <para type="description">
        /// Keep connections open after policy has been installed even if they are not allowed under
        /// the new policy. This overrides the settings in the Connection Persistence page. If you
        /// change this property, the change will not affect open connections, but only future connections.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter KeepConnectionsOpenAfterPolicyInstallation { get; set; }

        /// <summary>
        /// <para type="description">
        /// A value of true enables matching by the selected protocol's signature - the signature
        /// identifies the protocol as genuine. Select this option to limit the port to the specified
        /// protocol. If the selected protocol does not support matching by signature, this field
        /// cannot be set to true.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter MatchByProtocolSignature { get; set; }

        /// <summary>
        /// <para type="description">
        /// Indicates whether this service is used when 'Any' is set as the rule's service and there
        /// are several service objects with the same source port and protocol.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter MatchForAny { get; set; }

        /// <summary>
        /// <para type="description">
        /// Indicates whether this service is a Data Domain service which has been overridden.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter OverrideDefaultSettings { get; set; }

        /// <summary>
        /// <para type="description">
        /// The number of the port used to provide this service. To specify a port range, place a
        /// hyphen between the lowest and highest port numbers, for example 44-55.
        /// </para>
        /// </summary>
        [Parameter]
        public string Port { get; set; }

        /// <summary>
        /// <para type="description">
        /// Select the protocol type associated with the service, and by implication, the management
        /// server (if any) that enforces Content Security and Authentication for the service.
        /// Selecting a Protocol Type invokes the specific protocol handlers for each protocol type,
        /// thus enabling higher level of security by parsing the protocol, and higher level of
        /// connectivity by tracking dynamic actions (such as opening of ports).
        /// </para>
        /// </summary>
        [Parameter]
        public string Protocol { get; set; }

        /// <summary>
        /// <para type="description">Group object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceTCP { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">Time (in seconds) before the session times out.</para>
        /// </summary>
        [Parameter]
        public int SessionTimeout { get; set; }

        /// <summary>
        /// <para type="description">
        /// Port number for the client side service. If specified, only those Source port Numbers
        /// will be Accepted, Dropped, or Rejected during packet inspection. Otherwise, the source
        /// port is not inspected.
        /// </para>
        /// </summary>
        [Parameter]
        public string SourcePort { get; set; }

        /// <summary>
        /// <para type="description">
        /// Enables state-synchronised High Availability or Load Sharing on a ClusterXL or
        /// OPSEC-certified cluster.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter SyncConnectionsOnCluster { get; set; }

        /// <summary>
        /// <para type="description">Use default virtual session timeout.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter UseDefaultSessionTimeout { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceTCP);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var tcp = Session.UpdateServiceTCP(value);

            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case nameof(ServiceTCP): break;

                    case nameof(GroupAction):
                        if (GroupAction == MembershipActions.Replace && Groups == null)
                            tcp.Groups.Clear();
                        break;

                    case nameof(Groups):
                        tcp.Groups.Add(GroupAction, Groups);
                        break;

                    case nameof(TagAction):
                        if (TagAction == MembershipActions.Replace && Tags == null)
                            tcp.Tags.Clear();
                        break;

                    case nameof(Tags):
                        tcp.Tags.Add(TagAction, Tags);
                        break;

                    case nameof(NewName):
                        tcp.Name = NewName;
                        break;

                    default:
                        tcp.SetProperty(p, MyInvocation.BoundParameters[p]);
                        break;
                }
            }

            tcp.AcceptChanges(Ignore);

            WriteObject(tcp);
        }

        #endregion Methods
    }
}