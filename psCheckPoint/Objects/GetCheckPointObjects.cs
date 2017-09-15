using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for Get-CheckPoint*ObjectName*s classes</para>
    /// </summary>
    public abstract class GetCheckPointObjects<T> : GetCheckPointObjectsBase<CheckPointObjects<T>>
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
        public int UID { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [JsonProperty(PropertyName = "offset", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        public int Name { get; set; } = 0;

        //TODO order

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        protected string DetailsLevel { get; set; } = "full";
    }
}