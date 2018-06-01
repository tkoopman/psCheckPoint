using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="show-application-site">Get-CheckPointApplication</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing Application Site using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointApplication -Name "Check Point User Center"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplication", DefaultParameterSetName = "By Name or UID")]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationSite))]
    public class GetCheckPointApplication : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            if (ParameterSetName.Equals("By Application ID"))
                WriteObject(await Session.FindApplicationSite(ApplicationID, DetailsLevel, cancellationToken: CancelProcessToken));
            else
                WriteObject(await Session.FindApplicationSite(Value, DetailsLevel, cancellationToken: CancelProcessToken));
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// <para type="description">Object application identifier.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Application ID", ValueFromPipelineByPropertyName = true)]
        public int ApplicationID { get; set; }

        #endregion Properties
    }
}