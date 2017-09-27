using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace psCheckPoint.Objects.Policy
{
    /// <api cmd="install-policy">Install-CheckPointPolicy</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "CheckPointPolicy")]
    public class InstallCheckPointPolicy : CheckPointCmdlet<Dictionary<string, string>>
    {
        public override string Command { get { return "install-policy"; } }

        /// <summary>
        /// <para type="description">The name of the Policy Package to be installed.</para>
        /// </summary>
        [JsonProperty(PropertyName = "policy-package")]
        [Parameter(Mandatory = true)]
        public string PolicyPackage { get; private set; }

        /// <summary>
        /// <para type="description">Set to be true in order to install the access policy.</para>
        /// </summary>
        [JsonProperty(PropertyName = "access", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter Access { get; set; }

        /// <summary>
        /// <para type="description">install-on-all-cluster-members-or-fail</para>
        /// </summary>
        [JsonProperty(PropertyName = "install-on-all-cluster-members-or-fail", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter), new Object[] { true })]
        [Parameter]
        public SwitchParameter DisableInstallOnAllClusterMembersOrFail { get; set; }

        /// <summary>
        /// <para type="description">If true, prepares the policy for the installation, but doesn't install it on an installation target.</para>
        /// </summary>
        [JsonProperty(PropertyName = "prepare-only", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter PrepareOnly { get; set; }

        /// <summary>
        /// <para type="description">The UID of the revision of the policy to install.</para>
        /// </summary>
        [JsonProperty(PropertyName = "revision", NullValueHandling = NullValueHandling.Ignore)]
        [Parameter]
        public string Revision { get; set; }

        /// <summary>
        /// <para type="description">On what targets to execute this command. Targets may be identified by their name, or object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "targets", NullValueHandling = NullValueHandling.Ignore)]
        [Parameter]
        public string[] Targets { get; set; }

        /// <summary>
        /// <para type="description">Set to be true in order to install the threat prevention policy.</para>
        /// </summary>
        [JsonProperty(PropertyName = "threat-prevention", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter ThreatPrevention { get; set; }

        protected override void WriteRecordResponse(Dictionary<string, string> result)
        {
            WriteObject(result["task-id"]);
        }
    }
}