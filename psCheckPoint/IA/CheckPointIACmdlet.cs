using Koopman.CheckPoint;
using Koopman.CheckPoint.IA;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.IA
{
    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    public abstract class CheckPointIACmdlet : PSCmdletAsync
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// When using pipeline to send multiple requests at once, how many to batch together and
        /// send as single request.
        /// </para>
        /// </summary>
        [Parameter]
        public int BatchSize { get; set; } = 10;

        /// <summary>
        /// <para type="description">Server certificate hash you are expecting.</para>
        /// </summary>
        [Parameter]
        public string CertificateHash { get; set; }

        /// <summary>
        /// <para type="description">Do NOT verify server's certificate.</para>
        /// </summary>
        [Parameter]
        public CertificateValidation CertificateValidation { get; set; } = CertificateValidation.Auto;

        /// <summary>
        /// <para type="description">IP or Hostname of the gateway server.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Gateway { get; set; }

        /// <summary>
        /// <para type="description">Shared secret.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string SharedSecret { get; set; }

        /// <summary>
        /// Gets the IA session.
        /// </summary>
        /// <value>The IA session.</value>
        protected IASession Session { get; private set; }

        /// <summary>
        /// Gets the executed processing tasks the EndProcessingAsync will wait for.
        /// </summary>
        /// <value>The tasks.</value>
        protected List<Task> Tasks { get; } = new List<Task>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Begins the processing.
        /// </summary>
        protected sealed override void BeginProcessing()
        {
            Session = new IASession(Gateway, SharedSecret, certificateHash: CertificateHash, certificateValidation: CertificateValidation);
            base.BeginProcessing();
        }

        /// <summary>
        /// Ends the processing asynchronous.
        /// </summary>
        /// <returns></returns>
        protected override async Task EndProcessingAsync()
        {
            Tasks.Add(Session.Flush(true));
            await Task.WhenAll(Tasks);
            ((IDisposable)Session).Dispose();
        }

        #endregion Methods
    }
}