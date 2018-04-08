using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceUDP
{
    /// <api cmd="delete-service-udp">Remove-CheckPointServiceUDP</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointServiceUDP")]
    public class RemoveCheckPointServiceUDP : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject ServiceUDP { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(ServiceUDP);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Remove(string value)
        {
            Session.DeleteServiceUDP(value, Ignore);
        }

        #endregion Methods
    }
}