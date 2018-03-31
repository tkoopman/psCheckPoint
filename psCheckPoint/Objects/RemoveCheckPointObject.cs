using Koopman.CheckPoint.Common;
using System.Collections;
using System.ComponentModel;
using System.Management.Automation;
using System.Reflection;

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
        protected override void ProcessRecord()
        {
            ProcessObject(Object);
        }

        /// <summary>
        /// Removes the specified object.
        /// </summary>
        /// <param name="value">The name or UID of the object to remove.</param>
        protected abstract void Remove(string value);

        private void ProcessObject(object obj)
        {
            if (obj is string) Remove((obj as string));
            else if (obj is ObjectBase) Remove((obj as ObjectBase).GetMembershipID());
            else if (obj is PSObject) ProcessObject((obj as PSObject).BaseObject);
            else if (obj is IEnumerable)
            {
                foreach (object o in (obj as IEnumerable))
                    ProcessObject(o);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", InputName);
        }

        #endregion Methods
    }
}