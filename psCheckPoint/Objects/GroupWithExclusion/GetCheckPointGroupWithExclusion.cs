using System.Management.Automation;

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
        protected override void ProcessRecord() => WriteObject(Session.FindGroupWithExclusion(Value, DetailsLevel));

        #endregion Methods
    }
}