using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using psCheckPoint.Objects;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.Misc;
using psCheckPoint.Objects.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.Sync
{
    /// <extra category="Group Sync Commands">Invoke-CheckPointGroupSync</extra>
    /// <summary>
    /// <para type="synopsis">
    /// Performs a one way sync of external list of group members to a Check Point group.
    /// </para>
    /// <para type="description">
    /// Performs a one way sync of external list of group members to a Check Point group.
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// { ... } | Invoke-CheckPointGroupSync -Name MyGroup
    /// </code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Invoke, "CheckPointGroupSync")]
    [OutputType(typeof(SyncOutput))]
    public class InvokeCheckPointGroupSync : CheckPointCmdletBase
    {
        #region Fields

        private SortedList<string, IObjectSummary> _currentMembers = null;
        private SortedSet<string> _postMembers = null;
        private Group group = null;
        private ConsoleColor hostColor;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Color assigned to created objects</para>
        /// </summary>
        [Parameter]
        [Alias("Colour")]
        public Colors Color { get; set; } = Colors.Red;

        /// <summary>
        /// <para type="description">Comments for created objects</para>
        /// </summary>
        [Parameter]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">If group doesn't already exist create it.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter CreateGroup { get; set; }

        /// <summary>
        /// <para type="description">Name of group to be synced to list of members</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        [Alias("Name")]
        public string GroupName { get; set; }

        /// <summary>
        /// <para type="description">Used to ignore warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Ignore Ignore { get; set; }

        /// <summary>
        /// <para type="description">
        /// The list of all IPs and subnets that should exist in the group. Group members will be
        /// synced with this list.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Subnets", "IPAddresses")]
        public string[] Input { get; set; }

        /// <summary>
        /// <para type="description">The host and network object name prefix to use.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Prefix { get; set; }

        /// <summary>
        /// <para type="description">If object already exists but with different name, rename it.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Rename { get; set; }

        /// <summary>
        /// <para type="description">Tags assigned to created objects</para>
        /// </summary>
        [Parameter]
        public string[] Tags { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task BeginProcessingAsync()
        {
            hostColor = Host.UI.RawUI.ForegroundColor;
            try
            {
                group = await Session.FindGroup(GroupName, cancellationToken: CancelProcessToken);
            }
            catch (Koopman.CheckPoint.Exceptions.ObjectNotFoundException)
            {
                if (CreateGroup.IsPresent)
                {
                    group = new Group(Session)
                    {
                        Name = GroupName,
                        Color = Color,
                        Comments = Comments
                    };
                    foreach (string t in Tags ?? Enumerable.Empty<string>())
                        group.Tags.Add(t);

                    await group.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                }
            }

            if (group == null || group.UID == null)
                throw new PSArgumentException("Unable to find or create group", nameof(GroupName));

            _currentMembers = new SortedList<string, IObjectSummary>(group.Members.Count);
            foreach (var m in group.Members)
                _currentMembers.Add(m.Name.ToLower(), m);

            WriteVerbose($"Group currently contains {_currentMembers.Count} members.");

            _postMembers = new SortedSet<string>();
        }

        /// <inheritdoc />
        protected override async Task EndProcessingAsync()
        {
            string[] deleteKeys = _currentMembers.Keys.Except(_postMembers).ToArray();
            bool removed = true;

            if (deleteKeys.Length > 0)
            {
                WriteVerbose("Performing bulk remove from group");
                foreach (string n in deleteKeys)
                    group.Members.Remove(n);
                try
                {
                    await group.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                }
                catch (Koopman.CheckPoint.Exceptions.GenericException e)
                {
                    removed = false;
                    WriteError(new ErrorRecord(e, "RemoveFromGroup", ErrorCategory.WriteError, null));
                }
            }

            foreach (string key in deleteKeys)
            {
                var obj = _currentMembers[key];
                var output = new SyncOutput(obj.Name)
                {
                    Actions = Actions.Remove,
                    Error = !removed
                };

                var used = await Session.FindWhereUsed(identifier: obj.Name, detailLevel: DetailLevels.UID, cancellationToken: CancelProcessToken);
                if (used.UsedDirectly.Total == 0)
                {
                    output.Actions |= Actions.Delete;
                    try
                    {
                        if (obj is Host h)
                            await h.Delete(cancellationToken: CancelProcessToken);
                        else if (obj is Network n)
                            await n.Delete(cancellationToken: CancelProcessToken);
                    }
                    catch (Koopman.CheckPoint.Exceptions.GenericException e)
                    {
                        output.Error = true;
                        WriteError(new ErrorRecord(e, "Delete", ErrorCategory.WriteError, null));
                    }
                }
                else
                {
                    try
                    {
                        if (obj is Host h)
                        {
                            foreach (string t in Tags)
                                h.Tags.Remove(t);
                            await h.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                        }
                        else if (obj is Network n)
                        {
                            foreach (string t in Tags)
                                n.Tags.Remove(t);
                            await n.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                        }
                    }
                    catch (Koopman.CheckPoint.Exceptions.GenericException e)
                    {
                        output.Error = true;
                        WriteError(new ErrorRecord(e, "RemoveTags", ErrorCategory.WriteError, null));
                    }
                }

                WriteSyncOutput(output);
            }
        }

        /// <summary>
        /// Processes the record.
        /// </summary>
        /// <exception cref="PSInvalidCastException">Incorrect object found.</exception>
        protected override async Task ProcessRecordAsync()
        {
            foreach (string input in Input)
            {
                var member = new Member(Prefix, input, true);
                var output = new SyncOutput(member.Name);

                if (_currentMembers.ContainsKey(member.Name.ToLower()))
                {
                    // No changes just add to _postMembers so we don't remove it
                    _postMembers.Add(member.Name.ToLower());
                }
                else
                {
                    var obj = await FindObjectByName(member);
                    string oldName = null;
                    if (obj == null && Rename.IsPresent)
                    {
                        obj = await FindObjectByIP(member);
                        if (obj != null)
                        {
                            oldName = obj.Name;
                            output.Actions = Actions.Rename;
                            output.Comments = $"Renamed {obj.Name}";
                            try
                            {
                                if (obj is Network n)
                                {
                                    n.Name = member.Name;
                                    await n.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                                }
                                else if (obj is Host h)
                                {
                                    h.Name = member.Name;
                                    await h.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                                }
                            }
                            catch (Koopman.CheckPoint.Exceptions.GenericException e)
                            {
                                output.Error = true;
                                WriteError(new ErrorRecord(e, "Rename", ErrorCategory.WriteError, null));
                            }
                        }
                    }

                    if (obj == null)
                    {
                        output.Actions = Actions.Create;
                        try
                        {
                            if (member.IsHost())
                            {
                                var host = new Host(Session)
                                {
                                    Name = member.Name,
                                    Color = Color,
                                    Comments = Comments
                                };

                                if (member.IsIPv4())
                                    host.IPv4Address = member.IPAddress;
                                else
                                    host.IPv6Address = member.IPAddress;

                                host.Groups.Add(GroupName);
                                foreach (string t in Tags ?? Enumerable.Empty<string>())
                                    host.Tags.Add(t);

                                await host.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                            }
                            else
                            {
                                var network = new Network(Session)
                                {
                                    Name = member.Name,
                                    Color = Color,
                                    Comments = Comments
                                };

                                if (member.IsIPv4())
                                {
                                    network.Subnet4 = member.IPAddress;
                                    network.MaskLength4 = member.CIDR;
                                }
                                else
                                {
                                    network.Subnet6 = member.IPAddress;
                                    network.MaskLength6 = member.CIDR;
                                }

                                network.Groups.Add(GroupName);
                                foreach (string t in Tags ?? Enumerable.Empty<string>())
                                    network.Tags.Add(t);

                                await network.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                            }
                        }
                        catch (Koopman.CheckPoint.Exceptions.GenericException e)
                        {
                            output.Error = true;
                            WriteError(new ErrorRecord(e, "Create", ErrorCategory.WriteError, null));
                        }
                    }
                    else if (!_currentMembers.ContainsKey(obj.Name.ToLower()))
                    {
                        output.Actions |= Actions.Add;
                        group.Members.Add(obj.Name);
                        try
                        {
                            await group.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
                        }
                        catch (Koopman.CheckPoint.Exceptions.GenericException e)
                        {
                            output.Error = true;
                            WriteError(new ErrorRecord(e, "AddToGroup", ErrorCategory.WriteError, null));
                        }
                    }

                    // As _postMembers used to work out which entries to remove from the group If
                    // object was renamed enter old name as it could of already been a member Of the
                    // group under the old name
                    _postMembers.Add(((output.Actions.HasFlag(Actions.Rename)) ? oldName : member.Name).ToLower());
                }

                WriteSyncOutput(output);
            }
        }

        private async Task<IObjectSummary> FindObjectByIP(Member member)
        {
            try
            {
                if (member.IsHost())
                {
                    foreach (var host in await Session.FindAllHosts(member.IP, cancellationToken: CancelProcessToken))
                    {
                        if ((member.IsIPv4() && host.IPv4Address.Equals(member.IPAddress)) ||
                            (member.IsIPv6() && host.IPv6Address.Equals(member.IPAddress)))
                        {
                            return host;
                        }
                    }
                }
                else
                {
                    foreach (var network in await Session.FindAllNetworks(member.IP, cancellationToken: CancelProcessToken))
                    {
                        if ((member.IsIPv4() && network.Subnet4.Equals(member.IPAddress) && network.MaskLength4 == member.CIDR) ||
                            (member.IsIPv6() && network.Subnet6.Equals(member.IPAddress) && network.MaskLength6 == member.CIDR))
                        {
                            return network;
                        }
                    }
                }
            }
            catch (Koopman.CheckPoint.Exceptions.ObjectNotFoundException) { }

            return null;
        }

        private async Task<IObjectSummary> FindObjectByName(Member member)
        {
            try
            {
                if (member.IsHost())
                    return await Session.FindHost(member.Name, cancellationToken: CancelProcessToken);
                else
                    return await Session.FindNetwork(member.Name, cancellationToken: CancelProcessToken);
            }
            catch (Koopman.CheckPoint.Exceptions.ObjectNotFoundException)
            {
                return null;
            }
        }

        private void WriteSyncOutput(SyncOutput output)
        {
            if (output != null)
            {
                if (output.Error) Host.UI.RawUI.ForegroundColor = ConsoleColor.Red;
                WriteObject(output);
                if (output.Error) Host.UI.RawUI.ForegroundColor = hostColor;
            }
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// Actions that can be taken by Invoke-CheckPointSync to each group member.
        /// </summary>
        [Flags]
        public enum Actions
        {
            /// <summary>
            /// No changes required
            /// </summary>
            NoChange = 0,

            /// <summary>
            /// Renamed existing object
            /// </summary>
            Rename = 1,

            /// <summary>
            /// Added object to group
            /// </summary>
            Add = 2,

            /// <summary>
            /// Removed object from group
            /// </summary>
            Remove = 4,

            /// <summary>
            /// Created object and added to group
            /// </summary>
            Create = Add | 8,

            /// <summary>
            /// Deleted object no longer required and not used anywhere else
            /// </summary>
            Delete = Remove | 16
        };

        /// <summary>
        /// <para type="synopsis">Results of Invoke-CheckPointGroupSync.</para>
        /// <para type="description">Detailed results of group sync actions taken.</para>
        /// </summary>
        /// <example>
        /// <code>
        /// {Input IPs and Subnets} | Invoke-CheckPointGroupSync -Name MyGroup
        /// </code>
        /// </example>
        public class SyncOutput
        {
            #region Constructors

            internal SyncOutput(string name)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
            }

            #endregion Constructors

            #region Properties

            /// <summary>
            /// Gets the actions taken.
            /// </summary>
            /// <value>The actions.</value>
            public Actions Actions { get; internal set; }

            /// <summary>
            /// Gets the comments.
            /// </summary>
            /// <value>The comments.</value>
            public string Comments { get; internal set; }

            /// <summary>
            /// Gets a value indicating whether an error occured.
            /// </summary>
            /// <value><c>true</c> if error; otherwise, <c>false</c>.</value>
            public bool Error { get; internal set; } = false;

            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; private set; }

            #endregion Properties
        }

        /// <summary>
        /// Represents an input IP or Subnet
        /// </summary>
        private class Member
        {
            #region Fields

            private string _IP;

            #endregion Fields

            #region Constructors

            public Member(string name, string iP, bool nameIsPrefix = true)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                IP = iP ?? throw new ArgumentNullException(nameof(iP));

                if (nameIsPrefix)
                {
                    Name += IPAddress.ToString();
                    if (IsNetwork())
                        Name += $"/{CIDR}";
                }
            }

            #endregion Constructors

            #region Properties

            public int CIDR { get; private set; }

            public string IP
            {
                get => _IP;
                set
                {
                    string[] ipParts = value.Split('/');
                    IPAddress = IPAddress.Parse(ipParts[0]);
                    _IP = IPAddress.ToString();

                    if (!(IsIPv4() || IsIPv6()))
                        throw new InvalidCastException("Invalid IP address provided. Must be IPv4 or IPv6.");

                    if (ipParts.Length == 2)
                    {
                        CIDR = int.Parse(ipParts[1]);
                        if (CIDR <= 0 ||
                            (IsIPv4() && CIDR > 32) ||
                            (IsIPv6() && CIDR > 128))
                        {
                            throw new InvalidCastException("Invalid Mask Length provided.");
                        }
                    }
                    else if (IsIPv4())
                        CIDR = 32;
                    else if (IsIPv6())
                        CIDR = 128;
                }
            }

            public IPAddress IPAddress { get; private set; }
            public string Name { get; set; }

            #endregion Properties

            #region Methods

            public bool IsHost() => CIDR == ((IsIPv4()) ? 32 : 128);

            public bool IsIPv4() => (IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            public bool IsIPv6() => (IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);

            public bool IsNetwork() => !IsHost();

            #endregion Methods
        }

        #endregion Classes
    }
}