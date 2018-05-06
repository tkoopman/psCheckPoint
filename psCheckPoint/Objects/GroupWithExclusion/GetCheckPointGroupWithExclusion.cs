using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="show-group-with-exclusion">Get-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
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