using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="show-service-udp">Get-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceUDP")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceUDP))]
    public class GetCheckPointServiceUDP : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindServiceUDP(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}