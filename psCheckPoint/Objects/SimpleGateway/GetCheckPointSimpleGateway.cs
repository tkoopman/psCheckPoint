using System.Management.Automation;

namespace psCheckPoint.Objects.SimpleGateway
{
    /// <api cmd="show-simple-gateway">Get-CheckPointSimpleGateway</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSimpleGateway")]
    [OutputType(typeof(Koopman.CheckPoint.SimpleGateway))]
    public class GetCheckPointSimpleGateway : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.FindSimpleGateway(Value, DetailsLevel));
        }

        #endregion Methods
    }
}