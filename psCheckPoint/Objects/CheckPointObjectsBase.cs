﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

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
        [JsonProperty(PropertyName = "from")]
        public int From { get; set; }

        /// <summary>
        /// List of objects that other classes should implement a public property to expose.
        /// </summary>
        protected List<T> _Objects;

        /// <summary>
        /// <para type="description">To which element number the query was done.</para>
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public int To { get; set; }

        /// <summary>
        /// <para type="description">Total number of elements returned by the query.</para>
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_Objects).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_Objects).GetEnumerator();
        }
    }
}