using System.ComponentModel;
using System.Management.Automation;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for New-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class NewCheckPointCmdlet : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Color of the object. Should be one of existing colors.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Colour")]
        public Koopman.CheckPoint.Colors Color { get; set; } = Koopman.CheckPoint.Colors.Black;

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags
        {
            get { return _tags; }
            set { _tags = CreateArray(value); }
        }

        private string[] _tags;

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Koopman.CheckPoint.Ignore Ignore { get; set; } = Koopman.CheckPoint.Ignore.No;

        /// <summary>
        /// <para type="description">Return the updated object.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Writes the object only if PassThru is set.
        /// </summary>
        /// <param name="result">The result.</param>
        public new void WriteObject(object result)
        {
            if (PassThru.IsPresent) { base.WriteObject(result); }
        }
    }
}