using psCheckPoint.Session;
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
    /// <code>Get-CheckPointGroups | Get-CheckPointFullObject</code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointFullObject")]
    [OutputType(typeof(CheckPointObject))]
    public class GetCheckPointFullObject : PSCmdlet
    {
        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter]
        public CheckPointSession Session { get; set; }

        /// <summary>
        /// <para type="description">Input objects to start export from.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public PSObject Object { get; set; }

        protected override void BeginProcessing()
        {
            if (Session == null)
            {
                Session = SessionState.PSVariable.GetValue("CheckPointSession") as CheckPointSession;
                if (Session == null)
                {
                    throw new PSArgumentNullException("Session");
                }
            }
        }

        /// <summary>
        /// Provides a record-by-record processing functionality for the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            Process(Object);
        }

        private void Process(object obj)
        {
            if (obj is ICheckPointObjectSummary)
            {
                WriteObject((obj as ICheckPointObjectSummary).ToFullObj(Session));
            }
            else if (obj is PSObject)
            {
                Process((obj as PSObject).BaseObject);
            }
            else if (obj is IEnumerable)
            {
                foreach (object o in (obj as IEnumerable))
                {
                    Process(o);
                }
            }
            else
            {
                throw new CmdletInvocationException($"Invalid object type: {obj.GetType()}");
            }
        }
    }
}