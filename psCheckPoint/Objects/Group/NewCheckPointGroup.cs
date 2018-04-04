using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="add-group">New-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointGroup")]
    [OutputType(typeof(Koopman.CheckPoint.Group))]
    public class NewCheckPointGroup : NewCheckPointObject
    {
        #region Fields

        private string[] _groups;
        private string[] _members;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get => _groups; set => _groups = CreateArray(value); }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Members { get => _members; set => _members = CreateArray(value); }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var group = new Koopman.CheckPoint.Group(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments
            };

            foreach (var g in Groups ?? Enumerable.Empty<string>())
                group.Groups.Add(g);
            foreach (var m in Members ?? Enumerable.Empty<string>())
                group.Members.Add(m);
            foreach (var t in Tags ?? Enumerable.Empty<string>())
                group.Tags.Add(t);

            group.AcceptChanges(Ignore);

            WriteObject(group);
        }

        #endregion Methods
    }
}