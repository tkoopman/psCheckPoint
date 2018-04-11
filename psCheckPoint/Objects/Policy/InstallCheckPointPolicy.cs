using System.Management.Automation;

namespace psCheckPoint.Objects.Policy
{
    /// <api cmd="install-policy">Install-CheckPointPolicy</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "CheckPointPolicy")]
    public class InstallCheckPointPolicy : CheckPointCmdletBase
    {
        #region Fields

        private string[] _Targets;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Set to be true in order to install the access policy.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Access { get; set; }

        /// <summary>
        /// <para type="description">install-on-all-cluster-members-or-fail</para>
        /// </summary>
        [Parameter]
        public SwitchParameter DisableInstallOnAllClusterMembersOrFail { get; set; }

        /// <summary>
        /// <para type="description">The name of the Policy Package to be installed.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string PolicyPackage { get; private set; }

        /// <summary>
        /// <para type="description">
        /// If true, prepares the policy for the installation, but doesn't install it on an
        /// installation target.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter PrepareOnly { get; set; }

        /// <summary>
        /// <para type="description">The UID of the revision of the policy to install.</para>
        /// </summary>
        [Parameter]
        public string Revision { get; set; }

        /// <summary>
        /// <para type="description">
        /// On what targets to execute this command. Targets may be identified by their name, or
        /// object unique identifier.
        /// </para>
        /// </summary>
        [Parameter]
        public string[] Targets { get => _Targets; set => _Targets = CreateArray(value); }

        /// <summary>
        /// <para type="description">Set to be true in order to install the threat prevention policy.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ThreatPrevention { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.InstallPolicy(
                    policy: PolicyPackage,
                    targets: Targets,
                    access: Access.IsPresent,
                    threatPrevention: ThreatPrevention.IsPresent,
                    installOnAllClusterMembersOrFail: !DisableInstallOnAllClusterMembersOrFail.IsPresent,
                    prepareOnly: PrepareOnly.IsPresent,
                    revision: Revision
                ));
        }

        #endregion Methods
    }
}