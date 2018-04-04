using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="set-group-with-exclusion">Set-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Set, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(Koopman.CheckPoint.GroupWithExclusion))]
    public class SetCheckPointGroupWithExclusion : SetCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Object to exclude.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Except { get; set; }

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject GroupWithExclusion { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">Object to include.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Include { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(GroupWithExclusion);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var group = Session.UpdateGroupWithExclusion(value);

            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case nameof(GroupWithExclusion): break;

                    case nameof(Include):
                        group.SetInclude(Include);
                        break;

                    case nameof(Except):
                        group.SetExcept(Except);
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