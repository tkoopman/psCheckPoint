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
        public CheckPointAccessRule Rule { get; set; }

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
            export.Rules.Add(Rule);
        }

        protected override void EndProcessing()
        {
            foreach (CheckPointAccessRule rule in export.Rules)
            {
                foreach (CheckPointObject obj in rule.Source)
                {
                    Process(obj, 0);
                }
                foreach (CheckPointObject obj in rule.Destination)
                {
                    Process(obj, 0);
                }
            }
            WriteObject(export);
        }

        private void Process(CheckPointObject obj, int CurrentDepth)
        {
            switch (obj.Type)
            {
                case "host":
                    {
                        if (!export.Hosts.Contains(obj))
                        {
                            using (PowerShell PSI = PowerShell.Create())
                            {
                                PSI.AddCommand(new CmdletInfo("Get-CheckPointHost", typeof(GetCheckPointHost)));
                                PSI.AddParameter("Session", Session);
                                PSI.AddParameter("UID", obj.UID);

                                Collection<CheckPointHost> hosts = PSI.Invoke<CheckPointHost>();
                                foreach (CheckPointHost host in hosts)
                                {
                                    export.Hosts.Add(host);
                                }
                            }
                        }
                        break;
                    }
                case "group":
                    {
                        if (!export.Groups.Contains(obj))
                        {
                            using (PowerShell PSI = PowerShell.Create())
                            {
                                PSI.AddCommand(new CmdletInfo("Get-CheckPointGroup", typeof(GetCheckPointGroup)));
                                PSI.AddParameter("Session", Session);
                                PSI.AddParameter("UID", obj.UID);

                                Collection<CheckPointGroup> groups = PSI.Invoke<CheckPointGroup>();
                                foreach (CheckPointGroup group in groups)
                                {
                                    export.Groups.Add(group);
                                    if (CurrentDepth < Depth)
                                    {
                                        foreach (CheckPointObject member in group.Members)
                                        {
                                            Process(member, ++CurrentDepth);
                                        }
                                    }
                                    else
                                    {
                                        WriteVerbose("Depth limit reached!");
                                    }
                                }
                            }
                        }
                        break;
                    }
                default:
                    {
                        if (!export.Other.Contains(obj))
                        {
                            export.Other.Add(obj);
                        }
                        break;
                    }
            }
        }
    }
}