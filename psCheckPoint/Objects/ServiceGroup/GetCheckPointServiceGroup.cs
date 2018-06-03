using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="show-service-group">Get-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Service Group using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroup -Name MyServices
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceGroup")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceGroup))]
    public class GetCheckPointServiceGroup : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindServiceGroup(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}