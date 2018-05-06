using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for Set-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class SetCheckPointCmdlet : UpdateCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">New name of the object.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string NewName { get; set; }

        /// <summary>
        /// <para type="description">Action to take with tags.</para>
        /// </summary>
        [Parameter]
        public MembershipActions TagAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// Gets the type of object being deleted.
        /// </summary>
        protected abstract string InputName { get; }

        /// <summary>
        /// <para type="description">Object to set.</para>
        /// </summary>
        protected PSObject Object { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task ProcessRecordAsync() => ProcessObject(Object);

        /// <summary>
        /// Sets the specified object.
        /// </summary>
        /// <param name="value">The name or UID of the object to set.</param>
        protected abstract Task Set(string value);

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;
            if (name.Equals(InputName)) return true;

            switch (name)
            {
                case nameof(NewName):
                    obj.SetProperty("Name", NewName);
                    return true;

                case nameof(TagAction):
                    if (TagAction == MembershipActions.Replace && Tags == null)
                        obj.GetProperty<MemberMembershipChangeTracking<Tag>>("Tags").Clear();
                    return true;

                case nameof(Tags):
                    obj.GetProperty<MemberMembershipChangeTracking<Tag>>("Tags").Add(TagAction, Tags);
                    return true;

                default:
                    return false;
            }
        }

        private async Task ProcessObject(object obj)
        {
            CancelProcessToken.ThrowIfCancellationRequested();
            if (obj is string str) await Set(str);
            else if (obj is IObjectSummary o) await Set(o.GetIdentifier());
            else if (obj is PSObject pso) await ProcessObject(pso.BaseObject);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    await ProcessObject(eo);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", InputName);
        }

        #endregion Methods
    }
}