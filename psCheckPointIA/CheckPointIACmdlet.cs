using Newtonsoft.Json;
using psCheckPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPointIA
{
    /// <summary>
    /// <para type="synopsis">Queries the Identity Awareness associations of a given IP.</para>
    /// <para type="description"></para>
    /// </summary>
    public abstract class CheckPointIACmdlet : PSCmdlet
    {
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
        /// <para type="description">When using pipeline to send multiple requests at once, how many to batch together and send as single request.</para>
        /// </summary>
        [Parameter]
        public int BatchSize { get; set; } = 10;

        /// <summary>
        /// <para type="description">Do NOT verify server's certificate.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter NoCertificateValidation { get; set; }

        protected override void BeginProcessing()
        {
            if (NoCertificateValidation.IsPresent)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = null;
            }
        }
    }
}