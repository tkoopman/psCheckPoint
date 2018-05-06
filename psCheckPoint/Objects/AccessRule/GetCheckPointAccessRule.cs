using Koopman.CheckPoint;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AccessRule
{
    /// <api cmd="show-access-rule">Get-CheckPointAccessRule</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessRule")]
    [OutputType(typeof(Koopman.CheckPoint.AccessRule))]
    public class GetCheckPointAccessRule : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </para>
        /// </summary>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">Layer that the rule belongs to identified by the name or UID.</para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Layer { get; set; }

        /// <summary>
        /// <para type="description">Rule number.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Rule Number", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public int RuleNumber { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindAccessRule(Layer, RuleNumber, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}