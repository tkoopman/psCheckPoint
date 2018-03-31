using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;
using System.Net;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="set-host">Set-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointHost -Name Test1 -NewName Test2 -Tags TestTag
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointHost")]
    [OutputType(typeof(Koopman.CheckPoint.Host))]
    public class SetCheckPointHost : SetCheckPointCmdlet
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
        /// <para type="description">Host object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public new PSObject Host { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">
        /// IPv4 or IPv6 address. If both addresses are required use ipv4-address and ipv6-address
        /// fields explicitly.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// <para type="description">IPv4 address.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv4Address { get; set; }

        /// <summary>
        /// <para type="description">IPv6 address.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IPAddress IPv6Address { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(Host);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var host = Session.UpdateHost(value);

            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case nameof(Host): break;
                    case nameof(GroupAction):
                        if (GroupAction == MembershipActions.Replace && Groups == null)
                            host.Groups.Clear();
                        break;

                    case nameof(Groups):
                        host.Groups.Add(GroupAction, Groups);
                        break;

                    case nameof(IPAddress):
                        if (IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                            host.IPv6Address = IPAddress;
                        else
                            host.IPv4Address = IPAddress;
                        break;

                    case nameof(TagAction):
                        if (TagAction == MembershipActions.Replace && Tags == null)
                            host.Tags.Clear();
                        break;

                    case nameof(Tags):
                        host.Tags.Add(TagAction, Tags);
                        break;

                    case nameof(NewName):
                        host.Name = NewName;
                        break;

                    default:
                        host.SetProperty(p, MyInvocation.BoundParameters[p]);
                        break;
                }
            }

            host.AcceptChanges(Ignore);

            WriteObject(host);
        }
    }

    #endregion Methods
}