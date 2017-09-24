using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Result from commands that return multiple objects.</para>
    /// </summary>
    [JsonObject]
    public abstract class CheckPointObjectsBase<T> : IEnumerable<T>
    {
        /// <summary>
        /// <para type="description">From which element number the query was done.</para>
        /// </summary>
        [JsonProperty(PropertyName = "from", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int From { get; set; }

        /// <summary>
        /// List of objects that other classes should implement a public property to expose.
        /// </summary>
        protected List<T> _Objects;

        /// <summary>
        /// <para type="description">To which element number the query was done.</para>
        /// </summary>
        [JsonProperty(PropertyName = "to", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int To { get; set; }

        /// <summary>
        /// <para type="description">Total number of elements returned by the query.</para>
        /// </summary>
        [JsonProperty(PropertyName = "total", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of Objects.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_Objects).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of Objects.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_Objects).GetEnumerator();
        }
    }
}