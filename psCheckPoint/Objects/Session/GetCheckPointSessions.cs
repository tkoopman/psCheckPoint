using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-sessions">Get-CheckPointSessions</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSessions")]
    [OutputType(typeof(Koopman.CheckPoint.Common.NetworkObjectsPagingResults<Koopman.CheckPoint.SessionInfo>), ParameterSetName = new string[] { "Limit" })]
    [OutputType(typeof(Koopman.CheckPoint.SessionInfo[]), ParameterSetName = new string[] { "All" })]
    public class GetCheckPointSessions : GetCheckPointObjects
    {
        #region Properties

        /// <summary>
        /// <para type="description">Show a list of published sessions.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ViewPublishedSessions { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    await Session.FindSessions(
                            viewPublishedSessions: ViewPublishedSessions.IsPresent,
                            limit: Limit,
                            offset: Offset,
                            cancellationToken: CancelProcessToken), false);
            }
            else
            {
                WriteObject(
                    await Session.FindAllSessions(
                            viewPublishedSessions: ViewPublishedSessions.IsPresent,
                            limit: Limit,
                            cancellationToken: CancelProcessToken), true);
            }
        }

        #endregion Methods
    }
}