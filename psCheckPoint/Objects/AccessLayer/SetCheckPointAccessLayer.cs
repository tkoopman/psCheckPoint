using Koopman.CheckPoint.FastUpdate;
using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="set-access-layer">Set-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code>
    /// Set-CheckPointAccessLayer -Name Network -ApplicationsAndUrlFiltering $true
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "CheckPointAccessLayer")]
    [OutputType(typeof(Koopman.CheckPoint.AccessLayer))]
    public class SetCheckPointAccessLayer : SetCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID", "Layer")]
        public PSObject AccessLayer { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">
        /// Whether to enable Applications and URL Filtering blade on the layer.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool ApplicationsAndUrlFiltering { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether to enable Content Awareness blade on the layer.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool ContentAwareness { get; set; } = false;

        /// <summary>
        /// <para type="description">
        /// Whether to use X-Forward-For HTTP header, which is added by the proxy server to keep
        /// track of the original source IP.
        /// </para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool DetectUsingXForwardFor { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether to enable Firewall blade on the layer.</para>
        /// </summary>
        [DefaultValue(true)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Firewall { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to enable Mobile Access blade on the layer.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool MobileAccess { get; set; } = false;

        /// <summary>
        /// <para type="description">Whether this layer is shared.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Shared { get; set; } = false;

        /// <inheritdoc />
        protected override string InputName => nameof(AccessLayer);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var o = Session.UpdateAccessLayer(value);
            UpdateProperties(o);
            o.AcceptChanges(Ignore);
            WriteObject(o);
        }

        #endregion Methods
    }
}