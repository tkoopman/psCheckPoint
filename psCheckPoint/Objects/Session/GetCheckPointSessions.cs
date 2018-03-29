using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.Session
{
    /// <api cmd="show-sessions">Get-CheckPointSessions</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSessions")]
    [OutputType(typeof(Koopman.CheckPoint.Common.ObjectsPagingResults<Koopman.CheckPoint.SessionInfo>))]
    public class GetCheckPointSessions : GetCheckPointObjects
    {
        /// <summary>
        /// <para type="description">Show a list of published sessions.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ViewPublishedSessions { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            var results = Session.FindAllSessions(
                viewPublishedSessions: ViewPublishedSessions.IsPresent,
                limit: Limit,
                offset: Offset
                );

            if (ParameterSetName == "Limit")
            {
                WriteObject(results, false);
            }
            else
            {
                while (results != null)
                {
                    foreach (object r in results)
                    {
                        WriteObject(r);
                    }
                    results = results.NextPage();
                }
            }
        }
    }
}