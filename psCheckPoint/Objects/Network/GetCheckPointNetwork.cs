using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-network">Get-CheckPointNetwork</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Network using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointNetwork -Name MyNetwork
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetwork")]
    [OutputType(typeof(Koopman.CheckPoint.Network))]
    public class GetCheckPointNetwork : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindNetwork(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}