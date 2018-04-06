using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="show-service-udp">Get-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServiceUDP")]
    [OutputType(typeof(Koopman.CheckPoint.ServiceUDP))]
    public class GetCheckPointServiceUDP : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            WriteObject(Session.FindServiceUDP(Value, DetailsLevel));
        }

        #endregion Methods
    }
}