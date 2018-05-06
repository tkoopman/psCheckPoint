using Koopman.CheckPoint;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AccessRule
{
    /// <api cmd="show-access-rulebase">Get-CheckPointAccessRuleBase</api>
    /// <summary>
    /// <para type="synopsis">Shows the entire Access Rules layer.</para>
    /// <para type="description">
    /// Shows the entire Access Rules layer. This layer is divided into sections. An Access Rule may
    /// be within a section, or independent of a section (in which case it is said to be under the
    /// "global" section). The reply features a list of objects. Each object may be a section of the
    /// layer, with all its rules in, or a rule itself, for the case of rules which are under the
    /// global section. An optional "filter" field may be added in order to filter out only those
    /// rules that match a search criteria.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessRuleBase")]
    [OutputType(typeof(Koopman.CheckPoint.Common.AccessRulebasePagingResults))]
    public class GetCheckPointAccessRuleBase : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID", "Layer")]
        public PSObject AccessLayer { get; set; }

        /// <summary>
        /// <para type="description">
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </para>
        /// </summary>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">
        /// Search expression to filter the rulebase. The provided text should be exactly the same as
        /// it would be given in Smart Console. The logical operators in the expression ('AND', 'OR')
        /// should be provided in capital letters.
        /// </para>
        /// </summary>
        [Parameter()]
        public string Filter { get; set; }

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
        protected override async Task ProcessRecordAsync()
        {
            string layer;
            if (AccessLayer.BaseObject is string s)
                layer = s;
            else if (AccessLayer.BaseObject is Koopman.CheckPoint.AccessLayer al)
                layer = al.GetIdentifier();
            else
                throw new PSArgumentException($"Invalid value of type {AccessLayer.BaseObject.GetType()}. Should be string or AccessLayer object.", nameof(AccessLayer));

            WriteObject(await Session.FindAccessRulebase(layer, Filter, DetailsLevel, Limit, Offset, cancellationToken: CancelProcessToken));
        }

        #endregion Methods
    }
}