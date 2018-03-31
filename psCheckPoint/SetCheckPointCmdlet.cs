using Koopman.CheckPoint.Common;
using System.Collections;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for Set-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class SetCheckPointCmdlet : CheckPointCmdletBase
    {
        #region Fields

        private string[] _tags;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Color of the object. Should be one of existing colors.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Colour")]
        public Koopman.CheckPoint.Colors Color { get; set; } = Koopman.CheckPoint.Colors.Black;

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Koopman.CheckPoint.Ignore Ignore { get; set; } = Koopman.CheckPoint.Ignore.No;

        /// <summary>
        /// <para type="description">New name of the object.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string NewName { get; set; }

        /// <summary>
        /// <para type="description">Return the updated object.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">Action to take with tags.</para>
        /// </summary>
        [Parameter]
        public MembershipActions TagAction { get; set; } = MembershipActions.Replace;

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags { get => _tags; set => _tags = CreateArray(value); }

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
        protected override void ProcessRecord()
        {
            ProcessObject(Object);
        }

        /// <summary>
        /// Sets the specified object.
        /// </summary>
        /// <param name="value">The name or UID of the object to set.</param>
        protected abstract void Set(string value);

        /// <summary>
        /// Writes the object only if PassThru is set.
        /// </summary>
        /// <param name="result">The result.</param>
        protected new void WriteObject(object result)
        {
            if (PassThru.IsPresent) { base.WriteObject(result); }
        }

        private void ProcessObject(object obj)
        {
            if (obj is string) Set((obj as string));
            else if (obj is ObjectBase) Set((obj as ObjectBase).GetMembershipID());
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