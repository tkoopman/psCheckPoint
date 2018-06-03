using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="show-security-zone">Get-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Security Zone using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointSecurityZone -Name MyZone
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSecurityZone")]
    [OutputType(typeof(Koopman.CheckPoint.SecurityZone))]
    public class GetCheckPointSecurityZone : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindSecurityZone(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}