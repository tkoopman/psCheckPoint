using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="add-service-group">New-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointServiceGroup")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceGroup))]
    public class NewCheckPointServiceGroup : NewCheckPointObject
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
            var group = new Koopman.CheckPoint.ServiceGroup(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments
            };

            foreach (string g in Groups ?? Enumerable.Empty<string>())
                group.Groups.Add(g);
            foreach (string m in Members ?? Enumerable.Empty<string>())
                group.Members.Add(m);
            foreach (string t in Tags ?? Enumerable.Empty<string>())
                group.Tags.Add(t);

            group.AcceptChanges(Ignore);

            WriteObject(group);
        }

        #endregion Methods
    }
}