using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="show-service-tcp">Get-CheckPointServiceTCP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceTCP")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceTCP))]
    public class GetCheckPointServiceTCP : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.FindServiceTCP(Value, DetailsLevel));
        }

        #endregion Methods
    }
}