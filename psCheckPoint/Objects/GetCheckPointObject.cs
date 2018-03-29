using Koopman.CheckPoint;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Get-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class GetCheckPointObject : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">Object name or UID.</para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        [Alias("Name", "UID")]
        public string Value { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [Parameter]
        public DetailLevels DetailsLevel { get; set; } = DetailLevels.Standard;

        /// <inheritdoc/>
        protected abstract override void ProcessRecord();
    }
}