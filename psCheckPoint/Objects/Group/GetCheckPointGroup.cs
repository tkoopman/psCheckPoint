using System.Management.Automation;
using System.Threading.Tasks;

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
        protected override async Task ProcessRecordAsync() => WriteObject(await Session.FindGroup(Value, DetailsLevel, cancellationToken: CancelProcessToken));

        #endregion Methods
    }
}