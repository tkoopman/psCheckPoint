using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-sessions">Get-CheckPointSessions</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSessions")]
    [OutputType(typeof(Koopman.CheckPoint.Common.ObjectsPagingResults<Koopman.CheckPoint.SessionInfo>), ParameterSetName = new string[] { "Limit" })]
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
        protected override void ProcessRecord()
        {
            if (ParameterSetName == "Limit")
            {
                WriteObject(
                    Session.FindSessions(
                            viewPublishedSessions: ViewPublishedSessions.IsPresent,
                            limit: Limit,
                            offset: Offset), false);
            }
            else
            {
                WriteObject(
                    Session.FindAllSessions(
                            viewPublishedSessions: ViewPublishedSessions.IsPresent,
                            limit: Limit), false);
            }
        }

        #endregion Methods
    }
}