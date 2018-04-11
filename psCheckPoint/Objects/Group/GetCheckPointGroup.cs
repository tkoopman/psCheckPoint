using System.Management.Automation;

namespace psCheckPoint.Objects.Group
{
    /// <api cmd="show-group">Get-CheckPointGroup</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// $cpGroup = Get-CheckPointGroup -Name Test1
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroup")]
    [OutputType(typeof(Koopman.CheckPoint.Group))]
    public class GetCheckPointGroup : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => WriteObject(Session.FindGroup(Value, DetailsLevel));

        #endregion Methods
    }
}