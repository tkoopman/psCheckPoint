using Koopman.CheckPoint.FastUpdate;
using Newtonsoft.Json;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="set-service-group">Set-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointGroup -Name Test1 -NewName Test2 -Tags TestTag
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointServiceGroup")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceGroup))]
    public class SetCheckPointServiceGroup : SetCheckPointCmdlet
    {
        #region Fields

        private string[] _groups;
        private string[] _members;

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
        /// <para type="description">Action to take with members.</para>
        /// </summary>
        [Parameter]
        public MembershipActions MemberAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of Network objects identified by the name or UID.</para>
        /// <para type="description">
        /// Members listed will be either Added, Removed or replace the current list of members based
        /// on MemberAction parameter.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Members { get => _members; set => _members = CreateArray(value); }

        /// <summary>
        /// <para type="description">Group object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceGroup { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceGroup);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var group = Session.UpdateServiceGroup(value);

            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case nameof(ServiceGroup): break;

                    case nameof(GroupAction):
                        if (GroupAction == MembershipActions.Replace && Groups == null)
                            group.Groups.Clear();
                        break;

                    case nameof(Groups):
                        group.Groups.Add(GroupAction, Groups);
                        break;

                    case nameof(MemberAction):
                        if (MemberAction == MembershipActions.Replace && Groups == null)
                            group.Members.Clear();
                        break;

                    case nameof(Members):
                        group.Members.Add(MemberAction, Members);
                        break;

                    case nameof(TagAction):
                        if (TagAction == MembershipActions.Replace && Tags == null)
                            group.Tags.Clear();
                        break;

                    case nameof(Tags):
                        group.Tags.Add(TagAction, Tags);
                        break;

                    case nameof(NewName):
                        group.Name = NewName;
                        break;

                    default:
                        group.SetProperty(p, MyInvocation.BoundParameters[p]);
                        break;
                }
            }

            group.AcceptChanges(Ignore);

            WriteObject(group);
        }

        #endregion Methods
    }
}