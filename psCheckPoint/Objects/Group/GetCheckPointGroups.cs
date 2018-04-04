using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-groups">Get-CheckPointGroups</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroups
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroups")]
    [OutputType(typeof(Koopman.CheckPoint.Common.ObjectsPagingResults<Koopman.CheckPoint.Group>))]
    public class GetCheckPointGroups : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    Session.FindGroups(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel), false);
            }
            else
            {
                WriteObject(
                    Session.FindAllGroups(
                            limit: Limit,
                            detailLevel: DetailsLevel), false);
            }
        }

        #endregion Methods
    }
}