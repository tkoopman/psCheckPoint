using Koopman.CheckPoint;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="add-application-site-category">New-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Create new Application Category.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// New-CheckPointApplicationCategory -Name MyCategory
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointApplicationCategory")]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationCategory))]
    public class NewCheckPointApplicationCategory : NewCheckPointObject
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">A description for the application.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Collection of group identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Groups { get => _groups; set => _groups = CreateArray(value); }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            var o = new Koopman.CheckPoint.ApplicationCategory(Session, SetIfExists.IsPresent) { };
            UpdateProperties(o);
            await o.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            var o = (Koopman.CheckPoint.ApplicationCategory)obj;
            switch (name)
            {
                case nameof(Groups):
                    foreach (string g in Groups ?? Enumerable.Empty<string>())
                        o.Groups.Add(g);
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}