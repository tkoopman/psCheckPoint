using System.Collections.Generic;
using System.Management.Automation;

namespace psCheckPoint.Objects.Policy
{
    /// <api cmd="verify-policy">Test-CheckPointPolicy</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Test, "CheckPointPolicy")]
    public class TestCheckPointPolicy : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">The name of the Policy Package to be installed.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string PolicyPackage { get; private set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.VerifyPolicy(PolicyPackage));
        }

        #endregion Methods
    }
}