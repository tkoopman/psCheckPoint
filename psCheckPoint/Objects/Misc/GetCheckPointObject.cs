using Koopman.CheckPoint;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-object">Get-CheckPointObject</api>
    /// <summary>
    /// <para type="synopsis">Get object by UID.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointObject -UID "12345678-1234-1234-1234-123456789abc"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointObject")]
    [OutputType(typeof(Koopman.CheckPoint.IObjectSummary))]
    public class GetCheckPointObject : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </summary>
        /// <value>The details level.</value>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.FindObject(UID, DetailsLevel));
        }

        #endregion Methods
    }
}