﻿using Koopman.CheckPoint;
using System.Management.Automation;
using System.Threading.Tasks;

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
        /// <para type="description">
        /// The level of detail for some of the fields in the response can vary from showing only the
        /// UID value of the object to a fully detailed representation of the object.
        /// </para>
        /// </summary>
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
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindObject(UID, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}