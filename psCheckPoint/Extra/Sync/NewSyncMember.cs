using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.Sync
{
    /// <extra category="Group Sync Commands">New-SyncMember</extra>
    /// <summary>
    /// <para type="synopsis">Creates a member object used for pipelining into Invoke-CheckPointGroupSync detailing list of source members to sync.</para>
    /// </summary>
    /// <example>
    /// <code>{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup</code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "SyncMember", DefaultParameterSetName = "Prefix")]
    [OutputType(typeof(Member))]
    public class NewSyncMember : PSCmdlet
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = "Fixed Name")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Prefix")]
        public string Prefix { get; set; }

        /// <summary>
        /// <para type="description">IPv4 or IPv6 Host IP or Subnet CIDR.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [Alias(new string[] { "IPAddress", "IP", "Subnet" })]
        public string Address { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Prefix")
            {
                WriteObject(new Member(Prefix, Address));
            }
            else
            {
                WriteObject(new Member(Name, Address, false));
            }
        }
    }
}