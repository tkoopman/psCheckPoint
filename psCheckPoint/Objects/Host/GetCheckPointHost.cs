using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="show-host">Get-CheckPointHost</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing host using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointHost -Name MyHost
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointHost")]
    [OutputType(typeof(Koopman.CheckPoint.Host))]
    public class GetCheckPointHost : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindHost(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}