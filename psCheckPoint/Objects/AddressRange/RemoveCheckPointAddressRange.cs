using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="delete-address-range">Remove-CheckPointAddressRange</api>
    /// <summary>
    /// <para type="synopsis">Delete existing address range using name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Remove-CheckPointAddressRange -Name Range1
    /// </code>
    /// </example>
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
        protected override Task Remove(string value) => Session.DeleteAddressRange(value, Ignore, cancellationToken: CancelProcessToken);

        #endregion Methods
    }
}