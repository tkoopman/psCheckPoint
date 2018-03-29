using System.IO;
using System.Management.Automation;

namespace psCheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Debug a session.</para>
    /// <para type="description">Sets the DebugWriter to output debugs to a text file.</para>
    /// </summary>
    /// <example>
    ///   <code>Debug-CheckPointSession -FileName debug.txt</code>
    ///   <code>Debug-CheckPointSession -Disable</code>
    /// </example>
    [Cmdlet(VerbsDiagnostic.Debug, "CheckPointSession")]
    public class DebugCheckPointSession : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">The file name to outpout debug loggin to.</para>
        /// </summary>
        [Parameter(ParameterSetName = "On", Mandatory = true)]
        public string FileName { get; set; }

        /// <summary>
        /// Append to existing file.
        /// </summary>
        [Parameter(ParameterSetName = "On")]
        public SwitchParameter Append { get; set; }

        /// <summary>
        /// Force overwritting existing file.
        /// </summary>
        [Parameter(ParameterSetName = "On")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Disables debugging and closes the current output file.
        /// </summary>
        [Parameter(ParameterSetName = "Off", Mandatory = true)]
        public SwitchParameter Disable { get; set; }

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (ParameterSetName.Equals("Off"))
            {
                if (Session.DebugWriter != null)
                {
                    TextWriter tw = Session.DebugWriter;
                    Session.DebugWriter = null;

                    tw.Dispose();
                }
            }
            else
            {
                if (Session.DebugWriter != null)
                    throw new PSArgumentException("DebugWriter already set.", nameof(Session));
                if (Append.IsPresent)
                    Session.DebugWriter = File.AppendText(FileName);
                else if (Force.IsPresent || !File.Exists(FileName))
                    Session.DebugWriter = File.CreateText(FileName);
                else
                    throw new PSArgumentException("File name already exists. Use -Append or -Force to use existing file.", nameof(FileName));
            }
        }
    }
}