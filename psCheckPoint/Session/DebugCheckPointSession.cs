using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Debug a session.</para>
    /// <para type="description">Sets the DebugWriter to output debugs to a text file.</para>
    /// </summary>
    /// <example>
    /// <code>
    /// Debug-CheckPointSession -Path debug.txt
    /// </code>
    /// <code>
    /// Debug-CheckPointSession -Disable
    /// </code>
    /// </example>
    [Cmdlet(VerbsDiagnostic.Debug, "CheckPointSession")]
    public class DebugCheckPointSession : CheckPointCmdletBase
    {
        #region Properties

        /// <summary>
        /// <para type="description">Append to existing file.</para>
        /// </summary>
        [Parameter(ParameterSetName = "On")]
        public SwitchParameter Append { get; set; }

        /// <summary>
        /// <para type="description">Disables debugging and closes the current output file.</para>
        /// </summary>
        [Parameter(ParameterSetName = "Off", Mandatory = true)]
        public SwitchParameter Disable { get; set; }

        /// <summary>
        /// <para type="description">Force overwritting existing file.</para>
        /// </summary>
        [Parameter(ParameterSetName = "On")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">The file name to output debug logging to.</para>
        /// </summary>
        [Parameter(ParameterSetName = "On", Mandatory = true)]
        public string Path { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task BeginProcessingAsync()
        {
            if (ParameterSetName.Equals("Off"))
            {
                if (Session.DebugWriter != null)
                {
                    var tw = Session.DebugWriter;
                    Session.DebugWriter = null;

                    tw.Dispose();
                }
            }
            else
            {
                if (Session.DebugWriter != null)
                    throw new PSArgumentException("DebugWriter already set.", nameof(Session));
                if (Append.IsPresent)
                    Session.DebugWriter = File.AppendText(Path);
                else if (Force.IsPresent || !File.Exists(Path))
                    Session.DebugWriter = File.CreateText(Path);
                else
                    throw new PSArgumentException("File name already exists. Use -Append or -Force to use existing file.", nameof(Path));
            }

            return base.BeginProcessingAsync();
        }

        #endregion Methods
    }
}