using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-task">Get-CheckPointTask</api>
    /// <summary>
    /// <para type="synopsis">Retrieves task details by Task ID</para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointTask")]
    [OutputType(typeof(Koopman.CheckPoint.CheckPointTask))]
    public class GetCheckPointTask : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Unique identifier of task</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string TaskID { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindTask(TaskID, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}