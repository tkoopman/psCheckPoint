using psCheckPoint.Session;
using System;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Any object that can be expanded into the full object details from.</para>
    /// <para type="description">If object is already full details then just returns itself.</para>
    /// </summary>
    public interface ICheckPointObjectSummary
    {
        /// <summary>
        /// <para type="description">Check Point Web-API Type field</para>
        /// </summary>
        string Type { get; }

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <param name="Session">Current session used to get full details</param>
        /// <returns>Full details of object. If psCheckPoint doesn't implement the commands to get the full details of this object yet, returns this. If object not found then returns null.</returns>
        CheckPointObject ToFullObj(CheckPointSession Session);

        /// <summary>
        /// <para type="description">Return full object from summary</para>
        /// </summary>
        /// <typeparam name="T">Type object should be returned as.</typeparam>
        /// <param name="Session">Current session used to get full details.</param>
        /// <returns>Full details of object.</returns>
        /// <exception cref="InvalidCastException">If full object is not of type T.</exception>
        T ToFullObj<T>(CheckPointSession Session) where T : CheckPointObject;
    }
}