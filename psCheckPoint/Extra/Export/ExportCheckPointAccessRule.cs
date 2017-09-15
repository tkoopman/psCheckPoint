using psCheckPoint.Objects;
using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Objects.AddressRange;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.GroupWithExclusion;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.Misc;
using psCheckPoint.Objects.MulticastAddressRange;
using psCheckPoint.Objects.Network;
using psCheckPoint.Objects.Service;
using psCheckPoint.Objects.ServiceGroup;
using psCheckPoint.Objects.ServiceTCP;
using psCheckPoint.Objects.ServiceUDP;
using psCheckPoint.Session;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Extra.Export
{
    /// <summary>
    /// <para type="synopsis">Export input objects and any other object used by input objects.</para>
    /// <para type="description">Performs an export of input objects and any object used by an input object.</para>
    /// <para type="description">Input objects could be of the following types:</para>
    /// <para type="description">    * Any Check Point Object like Host, Network, Rule, etc</para>
    /// <para type="description">    * Output from Get-CheckPointWhereUsed</para>
    /// <para type="description">    * Output from Get-CheckPointObjects</para>
    /// <para type="description">    * An array or list of objects of any mixture of above</para>
    /// </summary>
    /// <example>
    /// <code>Export-CheckPointObjects -Session $Session -Verbose $InputObject1 $InputObject2 ... $InputObjectX | ConvertTo-CheckPointHtml -Open</code>
    /// </example>
    [Cmdlet(VerbsData.Export, "CheckPointObjects")]
    [OutputType(typeof(CheckPointExportSet))]
    public class ExportCheckPointObjects : Cmdlet
    {
        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public CheckPointSession Session { get; set; }

        /// <summary>
        /// <para type="description">Input objects to start export from.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        public PSObject Object { get; set; }

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
        /// <para type="description">Enter names of objects you do not want export to search for children of</para>
        /// </summary>
        [Parameter]
        public string[] ExcludeDetailsByName { get; set; } = { };

        /// <summary>
        /// <para type="description">Enter types of objects you do not want export to search for children of</para>
        /// </summary>
        [Parameter]
        [ValidateSet("group", "group-with-exclusion", "service-group", IgnoreCase = false)]
        public string[] ExcludeDetailsByType { get; set; } = { };

        /// <summary>
        /// <para type="description">Even if input object is not a rule do not perform a where used</para>
        /// </summary>
        [Parameter]
        public SwitchParameter SkipWhereUsed { get; set; }

        /// <summary>
        /// <para type="description">When passing Check Point objects as input perform a indirect where used instead of the standard direct only.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter IndirectWhereUsed { get; set; }

        private CheckPointExportSet export = new CheckPointExportSet();

        /// <summary>
        /// <para type="synopsis">Process each input object.</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            Process(Object.BaseObject, 0);
        }

        /// <summary>
        /// <para type="synopsis">Write out resulting export set object.</para>
        /// </summary>
        protected override void EndProcessing()
        {
            WriteObject(export);
        }

        private void Process(object obj, int CurrentDepth)
        {
            if (obj is ICheckPointObjectSummary)
            {
                Process(obj as ICheckPointObjectSummary, CurrentDepth);
            }
            else if (obj is PSObject)
            {
                Process((obj as PSObject).BaseObject, CurrentDepth);
            }
            else if (obj is CheckPointWhereUsed)
            {
                CheckPointWhereUsed o = obj as CheckPointWhereUsed;
                if (o.UsedDirectly != null)
                {
                    Process(o.UsedDirectly.Objects, CurrentDepth + 1);
                    Process(o.UsedDirectly.AccessControlRules, CurrentDepth);
                }

                if (o.UsedIndirectly != null)
                {
                    Process(o.UsedIndirectly.Objects, CurrentDepth + 1);
                    Process(o.UsedIndirectly.AccessControlRules, CurrentDepth);
                }
                //TODO add other entries when implemented fully
            }
            else if (obj is IEnumerable)
            {
                foreach (object o in (obj as IEnumerable))
                {
                    Process(o, CurrentDepth);
                }
            }
            else
            {
                throw new CmdletInvocationException($"Invalid object type: {obj.GetType()}");
            }
        }

        private void Process(ICheckPointObjectSummary obj, int CurrentDepth)
        {
            if (ExcludeByName.Contains(obj.ToString()) || ExcludeByType.Contains(obj.Type)) { return; }

            switch (obj.Type)
            {
                case "access-rule":
                    {
                        if (export.AccessRules.Contains(obj)) { return; }
                        CheckPointAccessRule r = obj.ToFullObj<CheckPointAccessRule>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "address-range":
                    {
                        if (export.AddressRanges.Contains(obj)) { return; }
                        CheckPointAddressRange r = obj.ToFullObj<CheckPointAddressRange>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "CpmiAnyObject": break;
                case "group":
                    {
                        if (export.Groups.Contains(obj)) { return; }
                        CheckPointGroup r = obj.ToFullObj<CheckPointGroup>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "group-with-exclusion":
                    {
                        if (export.GroupsWithExclusion.Contains(obj)) { return; }
                        CheckPointGroupWithExclusion r = obj.ToFullObj<CheckPointGroupWithExclusion>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "host":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointHost r = obj.ToFullObj<CheckPointHost>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "multicast-address-range":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointMulticastAddressRange r = obj.ToFullObj<CheckPointMulticastAddressRange>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "network":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointNetwork r = obj.ToFullObj<CheckPointNetwork>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-group":
                    {
                        if (export.ServiceGroups.Contains(obj)) { return; }
                        CheckPointServiceGroup r = obj.ToFullObj<CheckPointServiceGroup>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-tcp":
                    {
                        if (export.Services.Contains(obj)) { return; }
                        CheckPointServiceTCP r = obj.ToFullObj<CheckPointServiceTCP>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-udp":
                    {
                        if (export.Services.Contains(obj)) { return; }
                        CheckPointServiceUDP r = obj.ToFullObj<CheckPointServiceUDP>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                default:
                    {
                        if (obj is CheckPointObject)
                        {
                            CheckPointObject o = obj as CheckPointObject;
                            if (!export.Other.Contains(o))
                            {
                                WriteVerbose($"Exporting* {o.Type}: {o.Name}");
                                export.Other.Add(o);
                            }
                        }
                        break;
                    }
            }
        }

        private void Process(CheckPointAccessRule obj, int CurrentDepth)
        {
            if (export.AccessRules.Contains(obj)) { return; }

            string name = (String.IsNullOrWhiteSpace(obj.Name)) ? obj.UID : obj.Name;
            WriteVerbose($"Exporting {obj.Type}: {name}");

            export.AccessRules.Add(obj);

            if (CurrentDepth < Depth)
            {
                foreach (CheckPointObject newObj in obj.Source)
                {
                    Process(newObj, CurrentDepth + 1);
                }
                foreach (CheckPointObject newObj in obj.Destination)
                {
                    Process(newObj, CurrentDepth + 1);
                }
                foreach (CheckPointObject newObj in obj.Service)
                {
                    Process(newObj, CurrentDepth + 1);
                }
            }
            else
            {
                WriteVerbose("Depth limit reached!");
            }
        }

        private void Process(CheckPointAddressRange obj, int CurrentDepth)
        {
            if (export.AddressRanges.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.AddressRanges.Add(obj);
            WhereUsed(obj, CurrentDepth);
        }

        private void Process(CheckPointGroup obj, int CurrentDepth)
        {
            if (export.Groups.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.Groups.Add(obj);
            WhereUsed(obj, CurrentDepth);

            if (CurrentDepth < Depth && !ExcludeDetailsByName.Contains(obj.Name) && !ExcludeDetailsByType.Contains(obj.Type))
            {
                foreach (CheckPointObject member in obj.Members)
                {
                    Process(member, CurrentDepth + 1);
                }
            }
            else
            {
                WriteVerbose($"Not following {obj.Name}");
            }
        }

        private void Process(CheckPointGroupWithExclusion obj, int CurrentDepth)
        {
            if (export.GroupsWithExclusion.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.GroupsWithExclusion.Add(obj);
            WhereUsed(obj, CurrentDepth);

            if (CurrentDepth < Depth && !ExcludeDetailsByName.Contains(obj.Name) && !ExcludeDetailsByType.Contains(obj.Type))
            {
                Process(obj.Include, CurrentDepth + 1);
                Process(obj.Except, CurrentDepth + 1);
            }
            else
            {
                WriteVerbose($"Not following {obj.Name}");
            }
        }

        private void Process(CheckPointHost obj, int CurrentDepth)
        {
            if (export.Hosts.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.Hosts.Add(obj);
            WhereUsed(obj, CurrentDepth);
        }

        private void Process(CheckPointMulticastAddressRange obj, int CurrentDepth)
        {
            if (export.MulticastAddressRanges.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.MulticastAddressRanges.Add(obj);
            WhereUsed(obj, CurrentDepth);
        }

        private void Process(CheckPointNetwork obj, int CurrentDepth)
        {
            if (export.Networks.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.Networks.Add(obj);
            WhereUsed(obj, CurrentDepth);
        }

        private void Process(CheckPointService obj, int CurrentDepth)
        {
            if (export.Services.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.ToString()}");
            export.Services.Add(obj);
            WhereUsed(obj, CurrentDepth);
        }

        private void Process(CheckPointServiceGroup obj, int CurrentDepth)
        {
            if (export.ServiceGroups.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.ToString()}");
            export.ServiceGroups.Add(obj);
            WhereUsed(obj, CurrentDepth);

            if (CurrentDepth < Depth && !ExcludeDetailsByName.Contains(obj.Name) && !ExcludeDetailsByType.Contains(obj.Type))
            {
                Process(obj.Members, CurrentDepth + 1);
            }
            else
            {
                WriteVerbose($"Not following {obj.Name}");
            }
        }

        private void WhereUsed(CheckPointObject obj, int CurrentDepth)
        {
            // Only do this if an CheckPointObject was used as inital input
            if (CurrentDepth == 0 && !SkipWhereUsed.IsPresent)
            {
                WriteVerbose($"Performing where-used on {obj.ToString()}");
                CheckPointWhereUsed wu = GetCheckPointWhereUsed.Run(Session, obj, IndirectWhereUsed.IsPresent);
                Process(wu, CurrentDepth + 1);
            }
        }
    }
}