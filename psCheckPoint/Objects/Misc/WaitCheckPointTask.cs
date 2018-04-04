using Koopman.CheckPoint;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// <code>
    /// Install-CheckPointPolicy -PolicyPackage Standard -Targets MyGateway | Wait-CheckPointTask
    /// </code>
    /// </example>
    [Cmdlet(VerbsLifecycle.Wait, "CheckPointTask")]
    public class WaitCheckPointTask : CheckPointCmdletBase
    {
        #region Fields

        private CancellationTokenSource cancellationTokenSource;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Time in seconds to sleep in-between checking task status</para>
        /// </summary>
        [Parameter]
        public int SleepTime { get; set; } = 5;

        /// <summary>
        /// <para type="description">Unique identifier of task</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("TaskID")]
        public PSObject Task { get; set; }

        /// <summary>
        /// <para type="description">Timeout in seconds.</para>
        /// </summary>
        [Parameter]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 300;

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            ProcessObject(Task);
        }

        /// <summary>
        /// Stops the processing.
        /// </summary>
        protected override void StopProcessing() => cancellationTokenSource?.Cancel();

        private void ProcessObject(object obj)
        {
            if (obj is string str) Wait(str);
            else if (obj is Task t) Wait(t.TaskID);
            else if (obj is IReadOnlyDictionary<string, string> ro) ProcessObject(ro.Values);
            else if (obj is PSObject pso) ProcessObject(pso.BaseObject);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    ProcessObject(eo);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", nameof(Task));
        }

        /// <inheritdoc />
        private void Wait(string taskID)
        {
            var task = Session.FindTask(taskID);

            if (task.Status == Koopman.CheckPoint.Task.TaskStatus.InProgress)
            {
                var progress = new ProgressRecord(1, "Waiting for task to complete", task.TaskName);
                cancellationTokenSource = new CancellationTokenSource(Timeout * 1000);

                var waitTask = task.WaitAsync(
                        delay: SleepTime * 1000,
                        progress: new Progress<int>(i => progress.PercentComplete = i),
                        cancellationToken: cancellationTokenSource.Token
                    );

                while (waitTask.Status < System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    WriteProgress(progress);
                    Thread.Sleep(500);
                }

                cancellationTokenSource = null;

                progress.RecordType = ProgressRecordType.Completed;
                WriteProgress(progress);
            }
            WriteObject(task);
        }

        #endregion Methods
    }
}