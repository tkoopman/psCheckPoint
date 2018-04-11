using Koopman.CheckPoint;
using System.Collections;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <extra category="Misc.">Get-CheckPointFullObject</extra>
    /// <summary>
    /// <para type="synopsis">Retrieve full object details from object summary.</para>
    /// <para type="description">Many commands return lists of object summaries.</para>
    /// <para type="description">Use this to return the full objects for each summary.</para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroups | Get-CheckPointFullObject
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointFullObject")]
    public class GetCheckPointFullObject : PSCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </para>
        /// </summary>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">Input objects to start export from.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public PSObject Object { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Provides a record-by-record processing functionality for the cmdlet.
        /// </summary>
        protected override void ProcessRecord() => Process(Object);

        private void Process(object obj)
        {
            if (obj is IObjectSummary objectSummary)
            {
                objectSummary.Reload(OnlyIfPartial: false, detailLevel: DetailsLevel);
                WriteObject(obj);
            }
            else if (obj is PSObject)
            {
                Process((obj as PSObject).BaseObject);
            }
            else if (obj is IEnumerable)
            {
                foreach (object o in (obj as IEnumerable))
                    Process(o);
            }
            else throw new CmdletInvocationException($"Invalid object type: {obj.GetType()}");
        }

        #endregion Methods
    }
}