using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.Export
{
    /// <extra category="Export Commands">Export-CheckPointObjects</extra>
    /// <summary>
    /// <para type="synopsis">Export input objects and any other object used by input objects.</para>
    /// <para type="description">
    /// Performs an export of input objects and any object used by an input object.
    /// </para>
    /// <para type="description">Input objects could be of the following types:</para>
    /// <para type="description">* Any Check Point Object like Host, Network, Rule, etc</para>
    /// <para type="description">* Output from Get-CheckPointObjects</para>
    /// <para type="description">* Name of an object</para>
    /// <para type="description">* An array or list of objects of any mixture of above</para>
    /// </summary>
    /// <example>
    /// <code>
    /// Export-CheckPointObjects $InputObject1 $InputObject2 ... $InputObjectX
    /// </code>
    /// </example>
    [Cmdlet(VerbsData.Export, "CheckPointObjects")]
    [OutputType(typeof(string))]
    public class ExportCheckPointObjects : CheckPointCmdletBase
    {
        #region Enums

        /// <summary>
        /// Export output type
        /// </summary>
        public enum OutputType
        {
            /// <summary>
            /// JSON Data embeded in HTML file
            /// </summary>
            HTML,

            /// <summary>
            /// Raw JSON data
            /// </summary>
            JSON
        }

        #endregion Enums

        #region Fields

        private JsonExport export = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Max depth to look for objects used by input objects</para>
        /// </summary>
        [Parameter]
        [PSDefaultValue(Value = 3)]
        public int Depth { get; set; } = 3;

        /// <summary>
        /// <para type="description">Enter names of objects to exclude from export</para>
        /// </summary>
        [Parameter]
        public string[] ExcludeByName { get; set; } = { };

        /// <summary>
        /// <para type="description">Enter types of objects to exclude from export</para>
        /// </summary>
        [Parameter]
        [ValidateSet("object", "host", "network", "group", "address-range", "multicast-address-range", "group-with-exclusion", "simple-gateway", "security-zone", "time", "time-group", "access-role", "dynamic-object", "trusted-client", "tag", "dns-domain", "opsec-application",
            "service-tcp", "service-udp", "service-icmp", "service-icmp6", "service-sctp", "service-other", "service-group",
            IgnoreCase = false)]
        public string[] ExcludeByType { get; set; } = { };

        /// <summary>
        /// <para type="description">
        /// Enter names of objects you do not want export to search for children of
        /// </para>
        /// </summary>
        [Parameter]
        public string[] ExcludeDetailsByName { get; set; } = { };

        /// <summary>
        /// <para type="description">
        /// Enter types of objects you do not want export to search for children of
        /// </para>
        /// </summary>
        [Parameter]
        [ValidateSet("group", "group-with-exclusion", "service-group", IgnoreCase = false)]
        public string[] ExcludeDetailsByType { get; set; } = { };

        /// <summary>
        /// Force overwritting existing file.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Indent JSON data</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Indent { get; set; }

        /// <summary>
        /// <para type="description">
        /// When passing Check Point objects as input perform a indirect where used instead of the
        /// standard direct only.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter IndirectWhereUsed { get; set; }

        /// <summary>
        /// <para type="description">Input objects to start export from.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public PSObject Object { get; set; }

        /// <summary>
        /// Weather to output raw Json data or HTML.
        /// </summary>
        /// <value>The output type.</value>
        [Parameter]
        public OutputType Output { get; set; } = OutputType.HTML;

        /// <summary>
        /// <para type="description">Filename to write HTML file to.</para>
        /// </summary>
        [Parameter]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">
        /// Even if input object is not a rule do not perform a where used. NOTE: String inputs will
        /// ignore this and still run a Where Used.
        /// </para>
        /// </summary>
        [Parameter]
        public SwitchParameter SkipWhereUsed { get; set; }

        /// <summary>
        /// <para type="description">Filename to HTML file to use as template.</para>
        /// <para type="description">{JSON} will be replaced with JSON data.</para>
        /// <para type="description">Leave blank to use default template.</para>
        /// </summary>
        [Parameter]
        public string Template { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override Task BeginProcessingAsync()
        {
            if (Path != null && !Force.IsPresent && File.Exists(Path))
                throw new PSArgumentException("File already exists. Use -Force to use existing file.", nameof(Path));

            string dir = System.IO.Path.GetDirectoryName(Path);
            if (Path != null && !string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                throw new PSArgumentException("Path directory does not exist.", nameof(Path));

            export = new JsonExport(Session, ExcludeDetailsByType, ExcludeDetailsByName, ExcludeByType, ExcludeByName)
            {
                CancellationToken = CancelProcessToken
            };

            return base.BeginProcessingAsync();
        }

        /// <summary>
        /// <para type="synopsis">Write out resulting export set object.</para>
        /// </summary>
        protected async override Task EndProcessingAsync()
        {
            string json = await export.Export(Indent.IsPresent);
            string output = json;

            if (Output == OutputType.HTML)
            {
                if (string.IsNullOrWhiteSpace(Template))
                {
                    // Load default template
                    var _assembly = typeof(psCheckPoint.Extra.Export.ExportCheckPointObjects).GetTypeInfo().Assembly;
                    using (var _html = new StreamReader(_assembly.GetManifestResourceStream("psCheckPoint.Extra.Export.ExportTemplate.html")))
                    {
                        output = await _html.ReadToEndAsync();
                    }
                }
                else
                    output = File.ReadAllText(Template);

                if (string.IsNullOrWhiteSpace(output))
                    throw new CmdletInvocationException("Unable to load HTML template.");

                output = output.Replace("{JSON}", json);
            }

            if (Path != null)
                File.WriteAllText(Path, output);
            else
                WriteObject(output);

            WriteVerbose($"Exported {export.Count} objects");
        }

        /// <summary>
        /// <para type="synopsis">Process each input object.</para>
        /// </summary>
        protected override Task ProcessRecordAsync() => Process(Object.BaseObject);

        private async Task Process(object obj)
        {
            switch (obj)
            {
                case string str:
                    var whereUsed = await Session.FindWhereUsed(identifier: str, indirect: IndirectWhereUsed.IsPresent, cancellationToken: CancelProcessToken);
                    await export.AddAsync(str, whereUsed, Depth);
                    break;

                case IObjectSummary objectSummary:
                    await Process(objectSummary);
                    break;

                case PSObject psObject:
                    await Process(psObject.BaseObject);
                    break;

                case AccessRulebasePagingResults ruleBase:
                    await export.AddAsync(ruleBase, Depth);
                    break;

                case IEnumerable objs:
                    foreach (object o in objs)
                        await Process(o);
                    break;

                default:
                    throw new CmdletInvocationException($"Invalid input object type: {obj.GetType()}");
            }
        }

        private async Task Process(IObjectSummary obj)
        {
            if (ExcludeByName.Contains(obj.ToString()) || ExcludeByType.Contains(obj.Type)) { return; }

            await export.AddAsync(obj, Depth);
            if (!SkipWhereUsed.IsPresent)
                switch (obj)
                {
                    case AddressRange _:
                    case Group _:
                    case GroupWithExclusion _:
                    case Host _:
                    case MulticastAddressRange _:
                    case Network _:
                    case ServiceGroup _:
                    case ServiceDceRpc _:
                    case ServiceICMP _:
                    case ServiceICMP6 _:
                    case ServiceOther _:
                    case ServiceRPC _:
                    case ServiceSCTP _:
                    case ServiceTCP _:
                    case ServiceUDP _:
                        var whereUsed = await Session.FindWhereUsed(identifier: obj.GetIdentifier(), indirect: IndirectWhereUsed.IsPresent, cancellationToken: CancelProcessToken);
                        await export.AddAsync(obj.GetIdentifier(), whereUsed, Depth);
                        break;
                }
        }

        #endregion Methods
    }
}