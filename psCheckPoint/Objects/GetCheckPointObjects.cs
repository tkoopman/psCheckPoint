using Newtonsoft.Json;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Get-CheckPoint*ObjectName*s classes</para>
    /// </summary>
    public abstract class GetCheckPointObjects : GetCheckPointObjectsBase<CheckPointObjects>
    {
    }

    /// <summary>
    /// <para type="description">Base class for Get-CheckPoint*ObjectName*s classes</para>
    /// </summary>
    public abstract class GetCheckPointObjectsBase<T> : CheckPointCmdlet<T>
    {
        /// <summary>
        /// <para type="description">No more than that many results will be returned.</para>
        /// </summary>
        [JsonProperty(PropertyName = "limit", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PSDefaultValue(Value = 50)]
        [Parameter]
        [ValidateRange(1, 500)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [JsonProperty(PropertyName = "offset", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public int Offset { get; set; } = 0;

        //TODO order

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";
    }
}