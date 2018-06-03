using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="show-service-tcp">Get-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing TCP object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointServiceTCP -Name MyTCP
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceTCP")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceTCP))]
    public class GetCheckPointServiceTCP : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindServiceTCP(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}