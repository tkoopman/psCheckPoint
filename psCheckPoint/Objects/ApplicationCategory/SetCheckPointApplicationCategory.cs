using Koopman.CheckPoint;
using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="set-application-site-category">Set-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Edit existing Application Category using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointApplicationCategory -Name MyCategory -Color Red
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointApplicationCategory")]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationCategory))]
    public class SetCheckPointApplication : SetCheckPointCmdlet
    {
        #region Fields

        private string[] _groups;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Group object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ApplicationCategory { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">A description for the application.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Description { get; set; }

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

        /// <inheritdoc />
        protected override string InputName => nameof(ApplicationCategory);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task Set(string value)
        {
            var o = Session.UpdateApplicationCategory(value);
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
                case nameof(GroupAction):
                    if (GroupAction == MembershipActions.Replace && Groups == null)
                        o.Groups.Clear();
                    return true;

                case nameof(Groups):
                    o.Groups.Add(GroupAction, Groups);
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}