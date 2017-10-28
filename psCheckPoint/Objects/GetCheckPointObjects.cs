using Newtonsoft.Json;
using System.Collections;
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
    public abstract class GetCheckPointObjectsBase<T> : CheckPointCmdlet<T> where T : ICheckPointObjects, IEnumerable
    {
        /// <summary>
        /// <para type="description">No more than that many results will be returned.</para>
        /// </summary>
        [JsonProperty(PropertyName = "limit", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter]
        [ValidateRange(1, 500)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// <para type="description">Skip that many results before beginning to return them.</para>
        /// </summary>
        [JsonProperty(PropertyName = "offset", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ParameterSetName = "Limit")]
        public int Offset { get; set; } = 0;

        /// <summary>
        /// <para type="description">Get All Records</para>
        /// </summary>
        [Parameter(ParameterSetName = "All", Mandatory = true)]
        public SwitchParameter All { get; set; }

        //TODO order

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("standard")]
        protected string DetailsLevel { get; set; } = "standard";

        protected override void WriteRecordResponse(T result)
        {
            if (ParameterSetName == "Limit")
            {
                base.WriteRecordResponse(result);
            }
            else
            {
                foreach (object r in result)
                {
                    WriteObject(r);
                }

                if (result.To != result.Total)
                {
                    Offset = result.To;
                    ProcessRecord();
                }
            }
        }
    }
}