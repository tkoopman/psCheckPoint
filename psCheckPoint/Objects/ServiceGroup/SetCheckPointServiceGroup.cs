using Koopman.CheckPoint;
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
            var o = Session.UpdateServiceGroup(value);
            UpdateProperties(o);
            o.AcceptChanges(Ignore);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            var o = (Koopman.CheckPoint.ServiceGroup)obj;
            switch (name)
            {
                case nameof(GroupAction):
                    if (GroupAction == MembershipActions.Replace && Groups == null)
                        o.Groups.Clear();
                    return true;

                case nameof(Groups):
                    o.Groups.Add(GroupAction, Groups);
                    return true;

                case nameof(MemberAction):
                    if (MemberAction == MembershipActions.Replace && Members == null)
                        o.Members.Clear();
                    return true;

                case nameof(Members):
                    o.Members.Add(MemberAction, Members);
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}