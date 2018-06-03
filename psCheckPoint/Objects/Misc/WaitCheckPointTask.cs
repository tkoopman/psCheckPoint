using Koopman.CheckPoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

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
        protected override Task ProcessRecordAsync() => ProcessObject(Task);

        private async Task ProcessObject(object obj)
        {
            if (obj is string str) await Wait(str);
            else if (obj is CheckPointTask t) await Wait(t.TaskID);
            else if (obj is IReadOnlyDictionary<string, string> ro) await ProcessObject(ro.Values);
            else if (obj is PSObject pso) await ProcessObject(pso.BaseObject);
            else if (obj is IEnumerable enumerable)
            {
                foreach (object eo in enumerable)
                    await ProcessObject(eo);
            }
            else
                throw new PSArgumentException($"Invalid type: {obj.GetType()}", nameof(Task));
        }

        /// <inheritdoc />
        private async Task Wait(string taskID)
        {
            var task = await Session.FindTask(taskID);

            if (task.Status == CheckPointTask.TaskStatus.InProgress)
            {
                var progress = new ProgressRecord(1, "Waiting for task to complete", task.TaskName);
                var cts = new CancellationTokenSource(Timeout * 1000);

                await task.WaitAsync(
                        delay: SleepTime * 1000,
                        progress: new Progress<int>(i => { progress.PercentComplete = i; WriteProgress(progress); }),
                        cancellationToken: CancellationTokenSource.CreateLinkedTokenSource(CancelProcessToken, cts.Token).Token
                    );

                progress.RecordType = ProgressRecordType.Completed;
                WriteProgress(progress);
            }
            WriteObject(task);
        }

        #endregion Methods
    }
}