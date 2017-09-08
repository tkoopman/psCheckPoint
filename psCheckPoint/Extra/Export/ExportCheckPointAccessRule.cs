using psCheckPoint.Objects.AccessRule;
using psCheckPoint.Objects;
using psCheckPoint.Objects.AddressRange;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.GroupWithExclusion;
using psCheckPoint.Objects.Host;
using psCheckPoint.Objects.MulticastAddressRange;
using psCheckPoint.Objects.Network;
using psCheckPoint.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

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
                        CheckPointAccessRule r = ProcessCheckPointObject<CheckPointAccessRule>(obj, CurrentDepth, "Get-CheckPointAccessRule", typeof(GetCheckPointAccessRule));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "address-range":
                    {
                        if (export.AddressRanges.Contains(obj)) { return; }
                        CheckPointAddressRange r = ProcessCheckPointObject<CheckPointAddressRange>(obj, CurrentDepth, "Get-CheckPointAddressRange", typeof(GetCheckPointAddressRange));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "CpmiAnyObject": break;
                case "group":
                    {
                        if (export.Groups.Contains(obj)) { return; }
                        CheckPointGroup r = ProcessCheckPointObject<CheckPointGroup>(obj, CurrentDepth, "Get-CheckPointGroup", typeof(GetCheckPointGroup));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "group-with-exclusion":
                    {
                        if (export.GroupsWithExclusion.Contains(obj)) { return; }
                        CheckPointGroupWithExclusion r = ProcessCheckPointObject<CheckPointGroupWithExclusion>(obj, CurrentDepth, "Get-CheckPointGroupWithExclusion", typeof(GetCheckPointGroupWithExclusion));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "host":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointHost r = ProcessCheckPointObject<CheckPointHost>(obj, CurrentDepth, "Get-CheckPointHost", typeof(GetCheckPointHost));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "multicast-address-range":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointMulticastAddressRange r = ProcessCheckPointObject<CheckPointMulticastAddressRange>(obj, CurrentDepth, "Get-CheckPointMulticastAddressRange", typeof(GetCheckPointMulticastAddressRange));
                        if (r != null) { Process(r, CurrentDepth); }
                        break;
                    }
                case "network":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointNetwork r = ProcessCheckPointObject<CheckPointNetwork>(obj, CurrentDepth, "Get-CheckPointNetwork", typeof(GetCheckPointNetwork));
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

        private T ProcessCheckPointObject<T>(CheckPointObject obj, int CurrentDepth, string psCmdletName, Type psCmdlet) where T : CheckPointObject
        {
            if (obj is T) { return (T)obj; }

            using (PowerShell PSI = PowerShell.Create())
            {
                PSI.AddCommand(new CmdletInfo(psCmdletName, psCmdlet));
                PSI.AddParameter("Session", Session);
                PSI.AddParameter("UID", obj.UID);

                Collection<T> results = PSI.Invoke<T>();
                return results.First();
            }
        }
    }
}