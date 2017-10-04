using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Reflection;

#if NETFULL

using System.Web;

#elif NETCOREAPP1_1

using System.Text.Encodings.Web;
using System.Runtime.InteropServices;

#endif

namespace psCheckPoint.Extra.Export
{
    /// <extra category="Export Commands">ConvertTo-CheckPointHtml</extra>
    /// <summary>
    /// <para type="synopsis">Convert output from Export-CheckPointObjects to a HTML file.</para>
    /// <para type="description">Pipe results from Export-CheckPointObjects to here to create a HTML report of the exported data.</para>
    /// </summary>
    /// <example>
    /// <code>Export-CheckPointObjects -Verbose $InputObject | ConvertTo-CheckPointHtml -Open</code>
    /// </example>
    [Cmdlet(VerbsData.ConvertTo, "CheckPointHtml", DefaultParameterSetName = "ES0")]
    [OutputType(typeof(string))]
    public class ConvertToCheckPointHtml : PSCmdlet
    {
        /// <summary>
        /// <para type="description">Export set from Export-CheckPointObjects</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public CheckPointExportSet Export { get; set; }

        /// <summary>
        /// <para type="description">Filename to write HTML file to.</para>
        /// </summary>
        [Parameter]
        public string Out { get; set; }

        /// <summary>
        /// <para type="description">Open file afterwards in default browser.</para>
        /// <para type="description">If no filename provides then temp file will be created.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Open { get; set; }

        /// <summary>
        /// <para type="description">Filename to HTML file to use as template.</para>
        /// <para type="description">{JSON} will be replaced with JSON data.</para>
        /// <para type="description">Leave blank to use default template.</para>
        /// </summary>
        [Parameter]
        public string Template { get; set; }

        /// <summary>
        /// <para type="description">Indent the JSON data in the HTML output.</para>
        /// <para type="description">This will force the use of "Template Literals" which requires an ES6 compatible browser. (`{JSON}`)</para>
        /// </summary>
        [Parameter(ParameterSetName = "ES6")]
        public SwitchParameter IndentedJson { get; set; }

        /// <summary>
        /// <para type="description">Escape JSON text before inserting into HTML file.</para>
        /// <para type="description">Automatically turned on if template uses double quotes to define location for JSON. ("{JSON}")</para>
        /// </summary>
        [Parameter(ParameterSetName = "ES5")]
        public SwitchParameter EscapeJson { get; set; }

        /// <summary>
        /// <para type="description">Convert export set to JSON and insert into HTML template for output.</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            string html = null;
            if (String.IsNullOrWhiteSpace(Template))
            {
                // Load default template
                Assembly _assembly = typeof(psCheckPoint.Extra.Export.ConvertToCheckPointHtml).GetTypeInfo().Assembly;
                using (StreamReader _html = new StreamReader(_assembly.GetManifestResourceStream("psCheckPoint.Extra.Export.ExportTemplate.html")))
                {
                    html = _html.ReadToEnd();
                }
            }
            else
            {
                // Load template from file specified
                html = System.IO.File.ReadAllText(Template);
            }

            if (String.IsNullOrWhiteSpace(html))
            {
                throw new CmdletInvocationException("Unable to load HTML template.");
            }

            string json = JsonConvert.SerializeObject(Export, (IndentedJson.IsPresent) ? Formatting.Indented : Formatting.None);

            // See if we need to force ES6
            if (this.ParameterSetName.Equals("ES6"))
            {
                if (html.Contains("\"{JSON}\""))
                {
                    html = html.Replace("\"{JSON}\"", "`{JSON}`");
                }
            }
            else if (this.ParameterSetName.Equals("ES5") || html.Contains("\"{JSON}\""))
            {
#if NETFULL
                json = HttpUtility.JavaScriptStringEncode(json);
#elif NETCOREAPP1_1
                json = JavaScriptEncoder.Default.Encode(json);
#endif
            }

            html = html.Replace("{JSON}", json);

            if (Open.IsPresent && Out == null)
            {
                Out = Path.GetTempPath() + $"Export_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.html";
            }
            if (Out != null)
            {
                File.WriteAllText(Out, html);
                if (Open.IsPresent)
                {
                    OpenBrowser(Out);
                }
            }
            else
            {
                WriteObject(html);
            }
        }

        private void OpenBrowser(string url)
        {
#if NETFULL
            Process.Start(url);
#elif NETCOREAPP1_1
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //TODO Needs Testing
                WriteWarning("Using -Open on Linux is currently untested. Please let us know if this works or not.");
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //TODO Needs Testing
                WriteWarning("Using -Open on OSX is currently untested. Please let us know if this works or not.");
                Process.Start("open", url);
            }
            else
            {
                WriteWarning($"Using -Open is currently unsupported on {RuntimeInformation.OSDescription}. Please log issue ticket to add support.");
                WriteWarning($"You will need to open the file manually. File name: {url}");
            }
#endif
        }
    }
}