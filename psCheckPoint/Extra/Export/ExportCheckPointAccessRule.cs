using psCheckPoint.Objects;
using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Objects.AddressRange;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.GroupWithExclusion;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.MulticastAddressRange;
using psCheckPoint.Objects.Network;
using psCheckPoint.Objects.Service;
using psCheckPoint.Objects.ServiceGroup;
using psCheckPoint.Objects.ServiceTCP;
using psCheckPoint.Objects.ServiceUDP;
using psCheckPoint.Session;
using System;
using System.Linq;
using System.Management.Automation;

namespace psCheckPoint.Extra.Export
{
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
        /// <para type="description"></para>
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public PSObject Object { get; set; }

        /// <summary>
        /// <para type="description"></para>
        /// </summary>
        [Parameter]
        public int Depth { get; set; } = 3;

        /// <summary>
        /// <para type="description">Enter names of objects you do not want export to search for children of</para>
        /// </summary>
        [Parameter]
        public string[] ExcludeDetailsOn { get; set; } = { };

        private CheckPointExportSet export = new CheckPointExportSet();

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            Process(Object.BaseObject, 0);
        }

        protected override void EndProcessing()
        {
            WriteObject(export);
        }

        private void Process(object obj, int CurrentDepth)
        {
            if (obj is CheckPointObject)
            {
                Process(obj as CheckPointObject, CurrentDepth);
            }
            else if (obj is object[])
            {
                foreach (object o in (obj as object[]))
                {
                    Process(o, CurrentDepth);
                }
            }
            else if (obj is PSObject)
            {
                Process((obj as PSObject).BaseObject, CurrentDepth);
            }
            else
            {
                throw new CmdletInvocationException($"Invalid object type: {obj.GetType()}");
            }
        }

        private void Process(CheckPointObject obj, int CurrentDepth)
        {
            switch (obj.Type)
            {
                case "access-rule":
                    {
                        if (export.AccessRules.Contains(obj)) { return; }
                        CheckPointAccessRule r = obj.toFullObj<CheckPointAccessRule>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "address-range":
                    {
                        if (export.AddressRanges.Contains(obj)) { return; }
                        CheckPointAddressRange r = obj.toFullObj<CheckPointAddressRange>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "CpmiAnyObject": break;
                case "group":
                    {
                        if (export.Groups.Contains(obj)) { return; }
                        CheckPointGroup r = obj.toFullObj<CheckPointGroup>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "group-with-exclusion":
                    {
                        if (export.GroupsWithExclusion.Contains(obj)) { return; }
                        CheckPointGroupWithExclusion r = obj.toFullObj<CheckPointGroupWithExclusion>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "host":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointHost r = obj.toFullObj<CheckPointHost>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "multicast-address-range":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointMulticastAddressRange r = obj.toFullObj<CheckPointMulticastAddressRange>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "network":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointNetwork r = obj.toFullObj<CheckPointNetwork>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-group":
                    {
                        if (export.ServiceGroups.Contains(obj)) { return; }
                        CheckPointServiceGroup r = obj.toFullObj<CheckPointServiceGroup>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-tcp":
                    {
                        if (export.Services.Contains(obj)) { return; }
                        CheckPointServiceTCP r = obj.toFullObj<CheckPointServiceTCP>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "service-udp":
                    {
                        if (export.Services.Contains(obj)) { return; }
                        CheckPointServiceUDP r = obj.toFullObj<CheckPointServiceUDP>(Session);
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                default:
                    {
                        if (!export.Other.Contains(obj))
                        {
                            WriteVerbose($"Exporting* {obj.Type}: {obj.Name}");
                            export.Other.Add(obj);
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
        }

        private void Process(CheckPointGroup obj, int CurrentDepth)
        {
            if (export.Groups.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.Groups.Add(obj);

            if (CurrentDepth < Depth && !ExcludeDetailsOn.Contains(obj.Name))
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

            if (CurrentDepth < Depth && !ExcludeDetailsOn.Contains(obj.Name))
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
        }

        private void Process(CheckPointMulticastAddressRange obj, int CurrentDepth)
        {
            if (export.MulticastAddressRanges.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.MulticastAddressRanges.Add(obj);
        }

        private void Process(CheckPointNetwork obj, int CurrentDepth)
        {
            if (export.Networks.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.Name}");
            export.Networks.Add(obj);
        }

        private void Process(CheckPointService obj, int CurrentDepth)
        {
            if (export.Services.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.ToString()}");
            export.Services.Add(obj);
        }

        private void Process(CheckPointServiceGroup obj, int CurrentDepth)
        {
            if (export.ServiceGroups.Contains(obj)) { return; }

            WriteVerbose($"Exporting {obj.Type}: {obj.ToString()}");
            export.ServiceGroups.Add(obj);

            if (CurrentDepth < Depth && !ExcludeDetailsOn.Contains(obj.Name))
            {
                Process(obj.Members, CurrentDepth + 1);
            }
            else
            {
                WriteVerbose($"Not following {obj.Name}");
            }
        }
    }
}