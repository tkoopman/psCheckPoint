using Koopman.CheckPoint;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Get-CheckPoint*ObjectName*s classes</para>
    /// </summary>
    public abstract class GetCheckPointObjects : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Get All Records</para>
        /// </summary>
        [Parameter(ParameterSetName = "All", Mandatory = true)]
        public SwitchParameter All { get; set; }

        /// <summary>
        /// <para type="description">
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </para>
        /// </summary>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">No more than that many results will be returned.</para>
        /// </summary>
        [Parameter]
        [ValidateRange(1, 500)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [Parameter(ParameterSetName = "Limit")]
        public int Offset { get; set; } = 0;

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected abstract override void ProcessRecord();

        #endregion Methods
    }
}