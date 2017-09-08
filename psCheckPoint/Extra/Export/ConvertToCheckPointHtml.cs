using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace psCheckPoint.Extra.Export
{
    [Cmdlet(VerbsData.ConvertTo, "CheckPointHtml", DefaultParameterSetName = "ES0")]
    [OutputType(typeof(string))]
    public class ConvertToCheckPointHtml : PSCmdlet
    {
        /// <summary>
        /// <para type="description"></para>
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
        /// <para type="description">This will force the use of "Template Literals" which requires an ES6 compatibile browser. (`{JSON}`)</para>
        /// </summary>
        [Parameter(ParameterSetName = "ES6")]
        public SwitchParameter IndentedJson { get; set; }

        /// <summary>
        /// <para type="description">Escape JSON text before inserting into HTML file.</para>
        /// <para type="description">Automatically turned on if templated uses double quotes to define location for JSON. ("{JSON}")</para>
        /// </summary>
        [Parameter(ParameterSetName = "ES5")]
        public SwitchParameter EscapeJson { get; set; }

        protected override void ProcessRecord()
        {
            string html = null;
            if (String.IsNullOrWhiteSpace(Template))
            {
                // Load default template
                Assembly _assembly = Assembly.GetExecutingAssembly();
                StreamReader _html = new StreamReader(_assembly.GetManifestResourceStream("psCheckPoint.Extra.Export.ExportTemplate.html"));
                html = _html.ReadToEnd();
                _html.Close();
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
                json = HttpUtility.JavaScriptStringEncode(json);
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
                    System.Diagnostics.Process.Start(Out);
                }
            }
            else
            {
                WriteObject(html);
            }
        }
    }
}