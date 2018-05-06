using Koopman.CheckPoint;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.AccessLayer
{
    /// <api cmd="add-access-layer">New-CheckPointAccessLayer</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointAccessLayer")]
    [OutputType(typeof(Koopman.CheckPoint.AccessLayer))]
    public class NewCheckPointAccessLayer : NewCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Indicates whether to include a clean-up rule in the new layer.</para>
        /// </summary>
        [DefaultValue(true)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool AddDefaultRule { get; set; } = true;

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

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            var o = new Koopman.CheckPoint.AccessLayer(Session, AddDefaultRule);
            UpdateProperties(o);
            await o.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            //var o = (Koopman.CheckPoint.AccessLayer)obj;
            switch (name)
            {
                case nameof(AddDefaultRule):
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}