using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.ExportRule
{
    [Cmdlet(VerbsData.ConvertTo, "CheckPointHtml")]
    [OutputType(typeof(string))]
    public class ConvertToCheckPointHtml : Cmdlet
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

        protected override void ProcessRecord()
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();
            StreamReader _html = new StreamReader(_assembly.GetManifestResourceStream("psCheckPoint.Extra.ExportRule.ExportCheckPointAccessRule.html"));
            string html = _html.ReadToEnd().Replace("{JSON}", JsonConvert.SerializeObject(Export));
            _html.Close();

            if (Open.IsPresent && Out == null)
            {
                Out = Path.GetTempFileName() + ".html";
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