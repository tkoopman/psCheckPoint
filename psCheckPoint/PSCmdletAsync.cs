using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace psCheckPoint
{
    /// <summary>
    /// PSCmdlet that allows for asynchronous calls
    /// </summary>
    /// <seealso cref="System.Management.Automation.PSCmdlet" />
    public class PSCmdletAsync : PSCmdlet
    {
        #region Fields

        private PSSynchronizationContext psSyncCtx = new PSSynchronizationContext();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the cancel process token.
        /// </summary>
        /// <value>The cancel process token.</value>
        protected CancellationToken CancelProcessToken { get => CancelProcess.Token; }

        /// <summary>
        /// Gets or sets the cancellation token source used if the user cancels the call.
        /// </summary>
        /// <value>The cancellation token source.</value>
        private CancellationTokenSource CancelProcess { get; } = new CancellationTokenSource();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Begins the processing.
        /// </summary>
        protected override void BeginProcessing() => DoWithSyncCtx(BeginProcessingAsync);

        /// <summary>
        /// Begins the processing asynchronous.
        /// </summary>
        protected virtual Task BeginProcessingAsync() => Task.FromResult(true);

        /// <summary>
        /// Ends the processing.
        /// </summary>
        protected sealed override void EndProcessing() => DoWithSyncCtx(EndProcessingAsync);

        /// <summary>
        /// Ends the processing asynchronous.
        /// </summary>
        protected virtual Task EndProcessingAsync() => Task.FromResult(true);

        /// <summary>
        /// Processes the record.
        /// </summary>
        protected sealed override void ProcessRecord() => DoWithSyncCtx(ProcessRecordAsync);

        /// <summary>
        /// Processes the record asynchronous.
        /// </summary>
        protected virtual Task ProcessRecordAsync() => Task.FromResult(true);

        /// <summary>
        /// Stops the processing.
        /// </summary>
        protected sealed override void StopProcessing() => CancelProcess?.Cancel();

        private void DoWithSyncCtx(Func<Task> func)
        {
            var prevCtx = SynchronizationContext.Current;

            try
            {
                SynchronizationContext.SetSynchronizationContext(psSyncCtx);

                var task = func();
                task.ContinueWith(t => psSyncCtx.Complete(), TaskScheduler.Default);

                psSyncCtx.RunLoop();

                task.GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(prevCtx);
            }
        }

        #endregion Methods

        #region Classes

        private class PSSynchronizationContext : SynchronizationContext
        {
            #region Fields

            private readonly BlockingCollection<PSSynchronizationContextItem> queue =
                new BlockingCollection<PSSynchronizationContextItem>();

            #endregion Fields

            #region Methods

            public void Complete() => queue.Add(null); // Null means break

            public override void Post(SendOrPostCallback d, object state) => queue.Add(new PSSynchronizationContextItem(d, state));

            public void RunLoop()
            {
                while (queue.TryTake(out var item, Timeout.Infinite))
                {
                    if (item == null) break;

                    item.Callback(item.State);
                }
            }

            #endregion Methods
        }

        private class PSSynchronizationContextItem
        {
            #region Constructors

            public PSSynchronizationContextItem(SendOrPostCallback callback, object state)
            {
                Callback = callback;
                State = state;
            }

            #endregion Constructors

            #region Properties

            public SendOrPostCallback Callback { get; }
            public object State { get; }

            #endregion Properties
        }

        #endregion Classes
    }
}