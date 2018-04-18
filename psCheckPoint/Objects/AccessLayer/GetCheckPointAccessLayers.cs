using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="show-access-layers">Get-CheckPointAccessLayers</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointAccessLayers
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessLayers")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.AccessLayer>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.AccessLayer[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointAccessLayers : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    Session.FindAccessLayers(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel), false);
            }
            else
            {
                WriteObject(
                    Session.FindAllAccessLayers(
                            limit: Limit,
                            detailLevel: DetailsLevel), false);
            }
        }

        #endregion Methods
    }
}