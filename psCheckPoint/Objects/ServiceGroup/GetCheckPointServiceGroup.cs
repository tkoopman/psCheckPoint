using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceGroup
{
    /// <api cmd="show-service-group">Get-CheckPointServiceGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Get-CheckPointGroup -Name Test1
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceGroup")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceGroup))]
    public class GetCheckPointServiceGroup : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => WriteObject(Session.FindServiceGroup(Value, DetailsLevel));

        #endregion Methods
    }
}