using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="show-security-zone">Get-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Get, "CheckPointSecurityZone")]
    [OutputType(typeof(Koopman.CheckPoint.SecurityZone))]
    public class GetCheckPointSecurityZone : GetCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() => WriteObject(Session.FindSecurityZone(Value, DetailsLevel));

        #endregion Methods
    }
}