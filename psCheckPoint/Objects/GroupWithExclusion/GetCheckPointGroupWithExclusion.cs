using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="show-group-with-exclusion">Get-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Group with Exclusion using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroupWithExclusion -Name MyGroupWithExclusion
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(Koopman.CheckPoint.GroupWithExclusion))]
    public class GetCheckPointGroupWithExclusion : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindGroupWithExclusion(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}