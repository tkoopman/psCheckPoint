using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Threading;

namespace psCheckPoint.Objects.Misc
{
    /// <extra category="Misc.">Wait-CheckPointTask</extra>
    /// <summary>
    /// <para type="synopsis">Waits for task to complete.</para>
    /// <para type="description">Waits for task to complete then returns the completed task details.</para>
    /// </summary>
    /// <example>
    ///   <code>Install-CheckPointPolicy -Session $Session -PolicyPackage Standard -Targets MyGateway | Wait-CheckPointTask -Session $Session -Verbose</code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Wait, "CheckPointTask")]
    public class WaitCheckPointTask : GetCheckPointTask
    {
        /// <summary>
        /// <para type="description">Time in seconds to sleep in-between checking task status</para>
        /// </summary>
        [Parameter]
        public int SleepTime { get; set; } = 5;

        /// <summary>
        /// <para type="description">Timeout in seconds.</para>
        /// </summary>
        [Parameter]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 300;

        private Stopwatch watch;

        protected override void WriteRecordResponse(CheckPointTasks result)
        {
            if (watch == null)
            {
                watch = Stopwatch.StartNew();
            }
            CheckPointTask task = result.Tasks.First();
            if (task == null)
            {
                throw new System.Exception("Task not found");
            }

            if (task.ProgressPercentage == 100)
            {
                WriteVerbose($"Task {task.ProgressPercentage}% complete. Exiting.");
                WriteObject(task);
            }
            else if (watch.ElapsedMilliseconds >= Timeout * 1000)
            {
                WriteVerbose($"Task {task.ProgressPercentage}% complete. Timeout reached, exiting.");
                WriteObject(task);
            }
            else
            {
                WriteVerbose($"Task {task.ProgressPercentage}% complete");
                Thread.Sleep(SleepTime * 1000);
                this.ProcessRecord();
            }
        }
    }
}