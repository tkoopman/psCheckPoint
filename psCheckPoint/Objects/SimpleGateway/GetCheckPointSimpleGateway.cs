using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SimpleGateway
{
    /// <api cmd="show-simple-gateway">Get-CheckPointSimpleGateway</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSimpleGateway")]
    [OutputType(typeof(Koopman.CheckPoint.SimpleGateway))]
    public class GetCheckPointSimpleGateway : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindSimpleGateway(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}