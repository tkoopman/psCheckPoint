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
        /// <summary>
        /// <para type="description">Object name or UID.</para>
        /// </summary>
        [Parameter(ParameterSetName = "By Value", Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        [Alias("Name", "UID")]
        public string Value { get; set; }

        /// <summary>
        /// <para type="description">Object to delete.</para>
        /// </summary>
        [Parameter(ParameterSetName = "By Object", Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public PSObject Object { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Koopman.CheckPoint.Ignore Ignore { get; set; } = Koopman.CheckPoint.Ignore.No;

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (ParameterSetName.Equals("By Value"))
            {
                Remove(Value);
            }
            else
            {
                ProcessObject(Object);
            }
        }

        private void ProcessObject(object obj)
        {
            if (obj is ObjectBase) Remove((obj as ObjectBase).GetMembershipID());
            else if (obj is PSObject) ProcessObject((obj as PSObject).BaseObject);
            else if (obj is IEnumerable)
            {
                foreach (object o in (obj as IEnumerable))
                {
                    ProcessObject(o);
                }
            }
            else
                throw new CmdletInvocationException($"Invalid object type: {obj.GetType()}");
        }

        /// <summary>
        /// Removes the specified object.
        /// </summary>
        /// <param name="value">The name or UID of the object to remove.</param>
        protected abstract void Remove(string value);
    }
}