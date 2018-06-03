using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="add-group-with-exclusion">New-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Create new Group with Exclusion.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// New-CheckPointGroupWithExclusion -Name MyGroupWithExclusion -Include GroupA -Except GroupB
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(Koopman.CheckPoint.GroupWithExclusion))]
    public class NewCheckPointGroupWithExclusion : NewCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Object to exclude.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Except { get; set; }

        /// <summary>
        /// <para type="description">Object to include.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Include { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            var group = new Koopman.CheckPoint.GroupWithExclusion(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments
            };

            group.SetInclude(Include);
            group.SetExcept(Except);

            foreach (string t in Tags ?? Enumerable.Empty<string>())
                group.Tags.Add(t);

            await group.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);

            WriteObject(group);
        }

        #endregion Methods
    }
}