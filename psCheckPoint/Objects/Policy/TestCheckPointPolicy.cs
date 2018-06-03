using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Policy
{
    /// <api cmd="verify-policy">Test-CheckPointPolicy</api>
    /// <summary>
    /// <para type="synopsis">Verify policy</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Test-CheckPointPolicy -PolicyPackage MyPolicy
    /// </code>
    /// </example>
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
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.VerifyPolicy(PolicyPackage, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}