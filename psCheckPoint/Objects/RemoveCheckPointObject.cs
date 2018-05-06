using Koopman.CheckPoint;
using System.Collections;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Remove-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class RemoveCheckPointObject : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Koopman.CheckPoint.Ignore Ignore { get; set; } = Koopman.CheckPoint.Ignore.No;

        /// <summary>
        /// Gets the type of object being deleted.
        /// </summary>
        protected abstract string InputName { get; }

        /// <summary>
        /// <para type="description">Object to delete.</para>
        /// </summary>
        protected PSObject Object { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task ProcessRecordAsync() => ProcessObject(Object);

        /// <summary>
        /// Removes the specified object.
        /// </summary>
        /// <param name="value">The name or UID of the object to remove.</param>
        protected abstract Task Remove(string value);

        private async Task ProcessObject(object obj)
        {
            CancelProcessToken.ThrowIfCancellationRequested();
            if (obj is string str) await Remove(str);
            else if (obj is IObjectSummary o) await Remove(o.GetIdentifier());
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