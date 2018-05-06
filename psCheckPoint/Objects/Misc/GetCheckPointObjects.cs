using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-objects">Get-CheckPointObjects</api>
    /// <api cmd="show-unused-objects">Get-CheckPointObjects</api>
    /// <summary>
    /// <para type="synopsis">Find objects by Filter.</para>
    /// <para type="description">
    /// Can find many different types of objects based on a filter. Filters are same as what can be
    /// used in Smart Console
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointObjects -Filter "O365 OR Office365"
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    /// Get-CheckPointObjects -Unused
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointObjects", DefaultParameterSetName = GetCheckPointObjectsStatic.LimitFilter)]
    [OutputType(typeof(IObjectSummary), ParameterSetName = new string[] { GetCheckPointObjectsStatic.AllFilter, GetCheckPointObjectsStatic.AllUnused })]
    [OutputType(typeof(NetworkObjectsPagingResults<IObjectSummary>), ParameterSetName = new string[] { GetCheckPointObjectsStatic.LimitFilter, GetCheckPointObjectsStatic.LimitUnused })]
    public class GetCheckPointObjects : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Get All Records</para>
        /// </summary>
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllFilter, Mandatory = true)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllUnused, Mandatory = true)]
        public SwitchParameter All { get; set; }

        /// <summary>
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </summary>
        /// <value>The details level.</value>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <summary>
        /// <para type="description">
        /// Search expression to filter objects by. The provided text should be exactly the same as
        /// it would be given in Smart Console. The logical operators in the expression ('AND', 'OR')
        /// should be provided in capital letters. By default, the search involves both a textual
        /// search and a IP search. To use IP search only, set the "ip-only" parameter to true.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitFilter)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllFilter)]
        public string Filter { get; set; }

        /// <summary>
        /// <para type="description">
        /// If using "filter", use this field to search objects by their IP address only, without
        /// involving the textual search.
        /// </para>
        /// </summary>
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitFilter)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllFilter)]
        public SwitchParameter IPOnly { get; set; }

        /// <summary>
        /// <para type="description">No more than that many results will be returned.</para>
        /// </summary>
        [Parameter]
        [ValidateRange(1, 500)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitFilter)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitUnused)]
        public int Offset { get; set; } = 0;

        /// <summary>
        /// <para type="description">The objects' type</para>
        /// </summary>
        [DefaultValue("object")]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitFilter)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllFilter)]
        [ValidateSet("object", "host", "network", "group", "address-range", "multicast-address-range", "group-with-exclusion", "simple-gateway", "security-zone", "time", "time-group", "access-role", "dynamic-object", "trusted-client", "tag", "dns-domain", "opsec-application",
            "service-tcp", "service-udp", "service-icmp", "service-icmp6", "service-sctp", "service-other", "service-group",
            IgnoreCase = false)]
        public string Type { get; set; } = "object";

        /// <summary>
        /// <para type="description">Retrieve all unused objects.</para>
        /// </summary>
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.LimitUnused, Mandatory = true)]
        [Parameter(ParameterSetName = GetCheckPointObjectsStatic.AllUnused, Mandatory = true)]
        public SwitchParameter Unused { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            switch (ParameterSetName)
            {
                case GetCheckPointObjectsStatic.AllFilter:
                    WriteObject(await Session.FindAllObjects(
                        filter: Filter,
                        type: Type,
                        ipOnly: IPOnly.IsPresent,
                        detailLevel: DetailsLevel,
                        limit: Limit,
                        cancellationToken: CancelProcessToken
                        ));
                    break;

                case GetCheckPointObjectsStatic.AllUnused:
                    WriteObject(await Session.FindAllUnusedObjects(
                        detailLevel: DetailsLevel,
                        limit: Limit,
                        cancellationToken: CancelProcessToken
                        ));
                    break;

                case GetCheckPointObjectsStatic.LimitFilter:
                    WriteObject(await Session.FindObjects(
                        filter: Filter,
                        type: Type,
                        ipOnly: IPOnly.IsPresent,
                        detailLevel: DetailsLevel,
                        limit: Limit,
                        offset: Offset,
                        cancellationToken: CancelProcessToken
                        ));
                    break;

                case GetCheckPointObjectsStatic.LimitUnused:
                    WriteObject(await Session.FindUnusedObjects(
                        detailLevel: DetailsLevel,
                        limit: Limit,
                        offset: Offset,
                        cancellationToken: CancelProcessToken
                        ));
                    break;

                default:
                    throw new PSNotSupportedException($"Unknown parameter set name: {ParameterSetName}");
            }
        }

        #endregion Methods
    }

    internal static class GetCheckPointObjectsStatic
    {
        #region Fields

        internal const string AllFilter = "All + Filter";
        internal const string AllUnused = "All + Unused";
        internal const string LimitFilter = "Limit + Filter";
        internal const string LimitUnused = "Limit + Unused";

        #endregion Fields
    }
}