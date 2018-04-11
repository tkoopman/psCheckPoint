using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="delete-address-range">Remove-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointAddressRange")]
    public class RemoveCheckPointAddressRange : RemoveCheckPointObject
    {
        #region Properties

        /// <summary>
        /// <para type="description">Address Range object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject AddressRange { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(AddressRange);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Remove(string value) => Session.DeleteAddressRange(value, Ignore);

        #endregion Methods
    }
}