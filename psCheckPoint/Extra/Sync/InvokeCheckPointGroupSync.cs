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
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.Sync
{
    [Flags]
    public enum Actions
    {
        NoChange = 0,
        Rename = 1,
        Add = 2,
        Remove = 4,
        Create = Add | 8,
        Delete = Remove | 16
    };

    /// <summary>
    /// <para type="synopsis">Results of Invoke-CheckPointGroupSync.</para>
    /// <para type="description">Detailed results of group sync actions taken.</para>
    /// </summary>
    /// <example>
    /// <code>{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup</code>
    /// </example>
    public class SyncOutput
    {
        public SyncOutput(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }
        public Actions Actions { get; internal set; }
        public bool Error { get; internal set; } = false;
        public string Comments { get; internal set; }
    }

    /// <extra category="Group Sync Commands">Invoke-CheckPointGroupSync</extra>
    /// <summary>
    /// <para type="synopsis">Performs a one way sync of external list of group members to a Check Point group.</para>
    /// </summary>
    /// <example>
    /// <code>{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup</code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Invoke, "CheckPointGroupSync")]
    [OutputType(typeof(SyncOutput))]
    public class InvokeCheckPointGroupSync : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public Member[] Members { get; set; }

        /// <summary>
        /// <para type="description">Name of group to be synced to list of members</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">If object already exists but with different name, rename it.</para>
        /// </summary>
        [Parameter()]
        public SwitchParameter Rename { get; set; }

        /// <summary>
        /// <para type="description">When creating a new object passes IgnoreWarnings switch.</para>
        /// </summary>
        [Parameter()]
        public SwitchParameter IgnoreWarnings { get; set; }

        /// <summary>
        /// <para type="description">If group doesn't already exist create it.</para>
        /// </summary>
        [Parameter()]
        public SwitchParameter CreateGroup { get; set; }

        /// <summary>
        /// <para type="description">Color assigned to created objects</para>
        /// </summary>
        [Parameter]
        [Alias("Colour")]
        public string Color { get; set; } = "red";

        /// <summary>
        /// <para type="description">Comments for created objects</para>
        /// </summary>
        [Parameter]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">Tags assigned to created objects</para>
        /// </summary>
        [Parameter]
        public string[] Tags { get; set; }

        private CheckPointGroup _group = null;
        private SortedList<string, CheckPointObject> _currentMembers = null;
        private SortedSet<string> _postMembers = null;
        private SortedSet<string> _addMembers = null;

        private ConsoleColor hostColor;

        private PowerShell PSI = null;

        private void InvokePowerShell(string cmd, Type type, Dictionary<string, object> parameters = null)
        {
            if (PSI == null)
            {
                PSI = PowerShell.Create();
            }
            else
            {
                PSI.Commands.Clear();
                PSI.Streams.Error.Clear();
            }

            PSI.AddCommand(new CmdletInfo(cmd, type));
            PSI.AddParameter("Session", Session);
            if (parameters != null)
                PSI.AddParameters(parameters);

            PSI.Invoke();
        }

        private T InvokePowerShell<T>(string cmd, Type type, Dictionary<string, object> parameters = null)
        {
            if (PSI == null)
            {
                PSI = PowerShell.Create();
            }
            else
            {
                PSI.Commands.Clear();
                PSI.Streams.Error.Clear();
            }

            PSI.AddCommand(new CmdletInfo(cmd, type));
            PSI.AddParameter("Session", Session);
            if (parameters != null)
                PSI.AddParameters(parameters);

            Collection<T> results = PSI.Invoke<T>();

            if (results.Count > 0)
            {
                return results.First();
            }
            else
            {
                return default(T);
            }
        }

        private T[] InvokePowerShellArray<T>(string cmd, Type type, Dictionary<string, object> parameters = null)
        {
            if (PSI == null)
            {
                PSI = PowerShell.Create();
            }
            else
            {
                PSI.Commands.Clear();
                PSI.Streams.Error.Clear();
            }

            PSI.AddCommand(new CmdletInfo(cmd, type));
            PSI.AddParameter("Session", Session);
            if (parameters != null)
                PSI.AddParameters(parameters);

            Collection<T> results = PSI.Invoke<T>();

            if (results.Count > 0)
            {
                return results.ToArray();
            }
            else
            {
                return default(T[]);
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            hostColor = Host.UI.RawUI.ForegroundColor;

            _group = InvokePowerShell<CheckPointGroup>("Get-CheckPointGroup", typeof(GetCheckPointGroup),
                new Dictionary<string, object>
                {
                    { "Name", Name }
                }
            );

            if (_group == null && CreateGroup.IsPresent)
            {
                _group = InvokePowerShell<CheckPointGroup>("New-CheckPointGroup", typeof(NewCheckPointGroup),
                        new Dictionary<string, object>
                        {
                            { "Name", Name },
                            { "Color", Color },
                            { "Comments", Comments },
                            { "Tags", Tags },
                            { "PassThru", null }
                        }
                    );
                if (PSI.Streams.Error.Count > 0)
                {
                    throw new MethodInvocationException("Unable to create group.", PSI.Streams.Error[0].Exception);
                }
            }
            else if (_group == null)
            {
                throw new ItemNotFoundException("Group not found by name");
            }

            CheckPointObject[] currentMembers = _group.Members;

            _currentMembers = new SortedList<string, CheckPointObject>(currentMembers.Length);
            foreach (CheckPointObject m in currentMembers)
            {
                _currentMembers.Add(m.Name.ToLower(), m);
            }

            WriteVerbose($"Group currently contains {_currentMembers.Count} members.");

            _postMembers = new SortedSet<string>();
        }

        protected override void ProcessRecord()
        {
            foreach (Member m in Members)
            {
                SyncOutput output = new SyncOutput(m.Name);

                if (_currentMembers.ContainsKey(m.Name.ToLower()))
                {
                    // No changes just add to _postMembers so we don't remove it
                    _postMembers.Add(m.Name.ToLower());
                }
                else
                {
                    CheckPointObject obj = FindObjectByName(m);
                    string oldName = null;
                    if (obj == null && Rename.IsPresent)
                    {
                        obj = FindObjectByIP(m);
                        if (obj != null)
                        {
                            oldName = obj.Name;
                            output.Actions = Actions.Rename;
                            output.Comments = $"Renamed {obj.Name}";

                            InvokePowerShell(
                                (m.IsNetwork() ? "Set-CheckPointNetwork" : "Set-CheckPointHost"),
                                (m.IsNetwork() ? typeof(SetCheckPointNetwork) : typeof(SetCheckPointHost)),
                                new Dictionary<string, object>
                                {
                                    { "Name", oldName },
                                    { "NewName", m.Name },
                                    { "PassThru", null }
                                }
                            );
                            if (PSI.Streams.Error.Count > 0)
                            {
                                WriteError(PSI.Streams.Error[0]);
                                output.Comments += PSI.Streams.Error[0].ToString();
                                output.Error = true;
                            }
                        }
                    }

                    if (obj == null)
                    {
                        output.Actions = Actions.Create;
                        Dictionary<string, object> parameters = new Dictionary<string, object> {
                            { "Name", m.Name },
                            { "Groups", Name },
                            { "Color", Color },
                            { "Comments", Comments },
                            { "Tags", Tags },
                            { "IgnoreWarnings", IgnoreWarnings},
                        };
                        if (m.IsNetwork())
                        {
                            parameters.Add("Subnet", m.IP);
                            parameters.Add("MaskLength", m.CIDR);
                        }
                        else
                        {
                            parameters.Add("IPAddress", m.IP);
                        }

                        InvokePowerShell(
                            (m.IsNetwork() ? "New-CheckPointNetwork" : "New-CheckPointHost"),
                            (m.IsNetwork() ? typeof(NewCheckPointNetwork) : typeof(NewCheckPointHost)),
                            parameters
                        );

                        if (PSI.Streams.Error.Count > 0)
                        {
                            WriteError(PSI.Streams.Error[0]);
                            output.Comments += PSI.Streams.Error[0].ToString();
                            output.Error = true;
                        }
                    }
                    else if (!_currentMembers.ContainsKey(obj.Name.ToLower()))
                    {
                        output.Actions |= Actions.Add;
                        InvokePowerShell(
                            (m.IsNetwork() ? "Set-CheckPointNetwork" : "Set-CheckPointHost"),
                            (m.IsNetwork() ? typeof(SetCheckPointNetwork) : typeof(SetCheckPointHost)),
                            new Dictionary<string, object>
                            {
                                { "UID", obj.UID },
                                { "GroupAction", MembershipActions.Add },
                                { "Groups", _group.UID },
                                { "TagAction", MembershipActions.Add },
                                { "Tags", Tags }
                            }
                        );
                        if (PSI.Streams.Error.Count > 0)
                        {
                            WriteError(PSI.Streams.Error[0]);
                            output.Comments += PSI.Streams.Error[0].ToString();
                            output.Error = true;
                        }
                    }

                    // As _postMembers used to work out which entries to remove from the group
                    // If object was renamed enter old name as it could of already been a member
                    // Of the group under the old name
                    _postMembers.Add(((output.Actions.HasFlag(Actions.Rename)) ? oldName : m.Name).ToLower());
                }

                WriteSyncOutput(output);
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

        protected override void EndProcessing()
        {
            string[] deleteKeys = _currentMembers.Keys.Except(_postMembers).ToArray();
            bool performDeletes = false;

            if (deleteKeys.Length > 0)
            {
                WriteVerbose("Performing bulk remove from group");
                InvokePowerShell("Set-CheckPointGroup", typeof(SetCheckPointGroup),
                    new Dictionary<string, object>
                    {
                        { "UID", _group.UID },
                        { "MemberAction", MembershipActions.Remove },
                        { "Members", deleteKeys }
                    }
                );
                if (PSI.Streams.Error.Count > 0)
                {
                    WriteError(PSI.Streams.Error[0]);
                }
                else
                {
                    performDeletes = true;
                }
            }

            foreach (string key in deleteKeys)
            {
                CheckPointObject obj = _currentMembers[key];
                SyncOutput output = new SyncOutput(obj.Name);
                output.Actions = Actions.Remove;

                if (performDeletes)
                {
                    CheckPointWhereUsed used = GetCheckPointWhereUsed.Run(Session, obj, false);
                    if (used.UsedDirectly.Total == 0)
                    {
                        output.Actions |= Actions.Delete;
                        InvokePowerShell(
                            (obj.Type == "network") ? "Remove-CheckPointNetwork" : "Remove-CheckPointHost",
                            (obj.Type == "network") ? typeof(RemoveCheckPointNetwork) : typeof(RemoveCheckPointHost),
                            new Dictionary<string, object>
                            {
                                { "UID", obj.UID }
                            }
                        );
                    }
                    else
                    {
                        InvokePowerShell(
                            (obj.Type == "network") ? "Set-CheckPointNetwork" : "Set-CheckPointHost",
                            (obj.Type == "network") ? typeof(SetCheckPointNetwork) : typeof(SetCheckPointHost),
                            new Dictionary<string, object>
                            {
                                { "UID", obj.UID },
                                { "TagAction", MembershipActions.Remove },
                                { "Tags", Tags }
                            }
                        );
                        if (PSI.Streams.Error.Count > 0)
                        {
                            WriteError(PSI.Streams.Error[0]);
                            output.Comments += PSI.Streams.Error[0].ToString();
                            output.Error = true;
                        }
                    }
                }
                else
                {
                    output.Comments = "Failed to remove";
                    output.Error = true;
                }

                WriteSyncOutput(output);
            }

            if (PSI != null)
                PSI.Dispose();
        }

        private CheckPointObject FindObjectByName(Member member)
        {
            return InvokePowerShell<CheckPointObject>(
                (member.IsHost()) ? "Get-CheckPointHost" : "Get-CheckPointNetwork",
                (member.IsHost()) ? typeof(GetCheckPointHost) : typeof(GetCheckPointNetwork),
                new Dictionary<string, object>
                {
                    { "Name", member.Name }
                }
            );
        }

        private CheckPointObject FindObjectByIP(Member member)
        {
            CheckPointObject[] objs = InvokePowerShellArray<CheckPointObject>("Get-CheckPointObjects", typeof(psCheckPoint.Objects.Misc.GetCheckPointObjects),
                new Dictionary<string, object>
                {
                    { "Filter", member.IP },
                    { "Type", (member.IsHost()) ? "host" : "network" },
                    // { "IPOnly", null }, // Removed due to not working with IPv6
                    { "All", null }
                }
            );
            if (objs == null || objs.Length == 0)
            {
                return null;
            }
            else
            {
                // Will return first full match only
                foreach (CheckPointObject obj in objs)
                {
                    if (member.IsHost())
                    {
                        CheckPointHost host = obj.ToFullObj<CheckPointHost>(Session);
                        if ((member.IsIPv4() && host.IPv4Address == member.IP) ||
                            (member.IsIPv6() && host.IPv6Address == member.IP))
                        {
                            return host;
                        }
                    }
                    else
                    {
                        CheckPointNetwork network = obj.ToFullObj<CheckPointNetwork>(Session);
                        if ((member.IsIPv4() && network.Subnet4 == member.IP && network.MaskLength4 == member.CIDR) ||
                            (member.IsIPv6() && network.Subnet6 == member.IP && network.MaskLength6 == member.CIDR))
                        {
                            return network;
                        }
                    }
                }
            }

            return null;
        }
    }
}