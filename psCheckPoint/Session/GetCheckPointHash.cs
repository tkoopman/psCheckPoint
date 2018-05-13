using Koopman.CheckPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Session
{
    /// <summary>
    /// Used to get the certificate hash of a management server or gateway. Hash can then be used in
    /// other commands using the -CertificateHash parameter on commands like Open-CheckPointSession
    /// and Add-CheckPointIdentity.
    /// </summary>
    /// <seealso cref="System.Management.Automation.PSCmdlet" />
    [Cmdlet(VerbsCommon.Get, "CheckPointHash")]
    public class GetCheckPointHash : PSCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">TCP Port number if not the standard HTTPS port 443</para>
        /// </summary>
        [Parameter(Position = 1)]
        public int Port { get; set; } = 443;

        /// <summary>
        /// <para type="description">
        /// IP or Hostname of the Check point Management Server or Gateway to get hash for
        /// </para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Server { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Processes the record.
        /// </summary>
        protected override void ProcessRecord() => WriteObject(CertificateValidator.GetServerCertificateHash($"https://{Server}:{Port}"));

        #endregion Methods
    }
}