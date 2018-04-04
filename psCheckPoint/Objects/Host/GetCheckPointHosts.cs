using System.Management.Automation;

namespace psCheckPoint.Objects.Host
{
    /// <api cmd="show-hosts">Get-CheckPointHosts</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointHosts
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointHosts")]
    [OutputType(typeof(Koopman.CheckPoint.Common.ObjectsPagingResults<Koopman.CheckPoint.Host>))]
    public class GetCheckPointHosts : GetCheckPointObjects
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    Session.FindHosts(
                            limit: Limit,
                            offset: Offset,
                            detailLevel: DetailsLevel), false);
            }
            else
            {
                WriteObject(
                    Session.FindAllHosts(
                            limit: Limit,
                            detailLevel: DetailsLevel), false);
            }
        }

        #endregion Methods
    }
}