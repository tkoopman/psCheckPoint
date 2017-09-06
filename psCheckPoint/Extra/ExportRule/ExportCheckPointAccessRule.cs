using psCheckPoint.AccessControl_NAT.AccessRule;
using psCheckPoint.Objects;
using psCheckPoint.Objects.Group;
using psCheckPoint.Objects.Host;
using psCheckPoint.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.ExportRule
{
    [Cmdlet(VerbsData.Export, "CheckPointAccessRule")]
    [OutputType(typeof(CheckPointExportSet))]
    public class ExportCheckPointAccessRule : Cmdlet
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
                        if (export.Rules.Contains(obj)) { return; }
                        CheckPointAccessRule rule = ProcessCheckPointObject<CheckPointAccessRule>(obj, CurrentDepth, "Get-CheckPointAccessRule", typeof(GetCheckPointAccessRule));
                        if (rule != null) { Process(rule, CurrentDepth); }
                        break;
                    }
                case "host":
                    {
                        if (export.Hosts.Contains(obj)) { return; }
                        CheckPointHost host = ProcessCheckPointObject<CheckPointHost>(obj, CurrentDepth, "Get-CheckPointHost", typeof(GetCheckPointHost));
                        if (host != null) { Process(host, CurrentDepth); }
                        break;
                    }
                case "group":
                    {
                        if (export.Groups.Contains(obj)) { return; }
                        CheckPointGroup group = ProcessCheckPointObject<CheckPointGroup>(obj, CurrentDepth, "Get-CheckPointGroup", typeof(GetCheckPointGroup));
                        if (group != null) { Process(group, CurrentDepth); }
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

        private void Process(CheckPointAccessRule rule, int CurrentDepth)
        {
            if (export.Rules.Contains(rule)) { return; }

            string name = (String.IsNullOrWhiteSpace(rule.Name)) ? rule.UID : rule.Name;
            WriteVerbose($"Exporting access rule: {name}");

            export.Rules.Add(rule);

            if (CurrentDepth < Depth)
            {
                foreach (CheckPointObject newObj in rule.Source)
                {
                    Process(newObj, CurrentDepth + 1);
                }
                foreach (CheckPointObject newObj in rule.Destination)
                {
                    Process(newObj, CurrentDepth + 1);
                }
            }
            else
            {
                WriteVerbose("Depth limit reached!");
            }
        }

        private void Process(CheckPointGroup group, int CurrentDepth)
        {
            if (export.Groups.Contains(group)) { return; }

            WriteVerbose($"Exporting group: {group.Name}");
            export.Groups.Add(group);

            if (CurrentDepth < Depth)
            {
                foreach (CheckPointObject member in group.Members)
                {
                    Process(member, CurrentDepth + 1);
                }
            }
            else
            {
                WriteVerbose("Depth limit reached!");
            }
        }

        private void Process(CheckPointHost host, int CurrentDepth)
        {
            if (export.Hosts.Contains(host)) { return; }

            WriteVerbose($"Exporting host: {host.Name}");
            export.Hosts.Add(host);
        }

        private T ProcessCheckPointObject<T>(dynamic obj, int CurrentDepth, string psCmdletName, Type psCmdlet)
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