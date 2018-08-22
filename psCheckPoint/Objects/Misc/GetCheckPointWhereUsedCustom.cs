using Koopman.CheckPoint;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Misc
{
    /// <summary>
    /// <para type="synopsis">Searches for usage of the target object in other objects and rules.</para>
    /// <para type="description">
    /// This is a custom implementation that only makes non-indirect calls to the management server.
    /// It than manually follows certain object types to find indirect usages. This allows you to
    /// have where-used return less than the standard command in some instances. This will also be
    /// depricated if/when the standard Check Point API is updated with similar features.
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointWhereUsedCustom -Name http
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointWhereUsedCustom", DefaultParameterSetName = "By Object")]
    [OutputType(typeof(Koopman.CheckPoint.Common.WhereUsed))]
    public class GetCheckPointWhereUsedCustom : CheckPointCmdletBase
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
        /// <para type="description">Search for indirect usage.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Indirect { get; set; }

        /// <summary>
        /// <para type="description">Maximum nesting level during indirect usage search.</para>
        /// </summary>
        [DefaultValue(5)]
        [Parameter]
        public int IndirectMaxDepth { get; set; } = 5;

        /// <summary>
        /// <para type="description">
        /// Which object types should be followed. If not specified only group types will be followed.
        /// </para>
        /// </summary>
        [Parameter]
        public ObjectType[] IndirectTypes { get; set; }

        /// <summary>
        /// <para type="description">Check Point Object.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "By Object", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [DefaultValue(null)]
        public IObjectSummary Object { set => Value = value.UID; }

        /// <summary>
        /// <para type="description">Object name or UID.</para>
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "By Name or UID", Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        [Alias("Name", "UID")]
        public string Value { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindWhereUsedCustom(Value, DetailsLevel, Indirect, IndirectMaxDepth, IndirectTypes, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}