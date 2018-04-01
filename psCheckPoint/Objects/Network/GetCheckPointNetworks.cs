using System.Management.Automation;

namespace psCheckPoint.Objects.Network
{
    /// <api cmd="show-networks">Get-CheckPointNetworks</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointNetworks
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointNetworks")]
    [OutputType(typeof(Koopman.CheckPoint.Common.ObjectsPagingResults<Koopman.CheckPoint.Network>))]
    public class GetCheckPointNetworks : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var results = Session.FindAllNetworks(
                limit: Limit,
                offset: Offset,
                detailLevel: DetailsLevel
                );

            if (ParameterSetName == "Limit")
            {
                WriteObject(results, false);
            }
            else
            {
                while (results != null)
                {
                    foreach (var r in results)
                    {
                        WriteObject(r);
                    }
                    results = results.NextPage();
                }
            }
        }

        #endregion Methods
    }
}