using System.Management.Automation;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="show-application-site">Get-CheckPointApplication</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplication", DefaultParameterSetName = "By Name or UID")]
    [OutputType(typeof(Koopman.CheckPoint.ApplicationSite))]
    public class GetCheckPointApplication : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (ParameterSetName.Equals("By Application ID"))
                WriteObject(Session.FindApplicationSite(ApplicationID, DetailsLevel));
            else
                WriteObject(Session.FindApplicationSite(Value, DetailsLevel));
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