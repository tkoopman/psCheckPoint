using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Objects
{
    public interface ICheckPointObjects
    {
        /// <summary>
        /// <para type="description">From which element number the query was done.</para>
        /// </summary>
        int From { get; set; }

        /// <summary>
        /// <para type="description">To which element number the query was done.</para>
        /// </summary>
        int To { get; set; }

        /// <summary>
        /// <para type="description">Total number of elements returned by the query.</para>
        /// </summary>
        int Total { get; set; }
    }
}