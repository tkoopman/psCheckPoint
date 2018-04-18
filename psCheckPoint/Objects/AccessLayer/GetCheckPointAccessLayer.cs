using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="show-access-layer">Get-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointAccessLayer -Name Network
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAccessLayer")]
    [OutputType(typeof(Koopman.CheckPoint.AccessLayer))]
    public class GetCheckPointAccessLayer : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => WriteObject(Session.FindAccessLayer(Value, DetailsLevel));

        #endregion Methods
    }
}