using Newtonsoft.Json;
using psCheckPoint.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml.Linq;

namespace DocGen
{
    internal class content_json
    {
        private static Dictionary<string, APICmdlet> cmds;
        private static Dictionary<string, ExtraCmdlet> extraCmds;

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error: Enter path");
                Console.ReadKey();

                return;
            }
            string path = args[0];
            Console.WriteLine($"Output path: {path}");

            CreateContentJson(path);

            CreateExtraContentJson(path);

            Console.ReadKey();
        }

        private static void CreateContentJson(string path)
        {
            cmds = getImplementedCmdlets();
            OutputChapter output = new OutputChapter("chapters");

            // Get List of all Check Point API calls
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = client.GetAsync($"https://sc1.checkpoint.com/documents/latest/APIs/data/v1.1/dynamic/content.json").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string strJson = response.Content.ReadAsStringAsync().Result;

                        dynamic content = JsonConvert.DeserializeObject(strJson);
                        foreach (dynamic chapter in content["chapters"])
                        {
                            ProcessChapter(chapter, output);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error getting content.json");
                    }
                }
            }

            // Remove empty chapters
            foreach (OutputChapter chapter in output.SubChapters.ToList())
            {
                if (chapter.SubChapters.Count == 0 && chapter.Cmdlets.Count == 0)
                {
                    output.SubChapters.Remove(chapter);
                }
            }

            // Output results to file
            File.WriteAllText($@"{path}\content.json", JsonConvert.SerializeObject(output.SubChapters, Formatting.Indented));
        }

        private static void CreateExtraContentJson(string path)
        {
            extraCmds = getImplementedExtraCmdlets();
            OutputChapter output = new OutputChapter("chapters");

            var list = extraCmds.Keys.ToList();
            list.Sort();

            OutputChapter current = null;
            foreach (string i in list)
            {
                string[] keys = i.Split('/');
                if (current == null || !current.Name.Equals(keys[0]))
                {
                    Console.WriteLine(keys[0]);
                    current = new OutputChapter(keys[0], output);
                }

                Console.WriteLine(keys[1]);
                new OutputCmdlet(current, extraCmds[i].cmdlet, extraCmds[i].type.Name, extraCmds[i].cmdlet);
            }

            // Output results to file
            File.WriteAllText($@"{path}\extra-content.json", JsonConvert.SerializeObject(output.SubChapters, Formatting.Indented));
        }

        private static void ProcessChapter(dynamic chapter, OutputChapter current)
        {
            Console.WriteLine(chapter["name"]);
            OutputChapter c = new OutputChapter(chapter["name"].ToString(), current);

            foreach (dynamic cmd in chapter["commands-data"])
            {
                ProcessCommandData(cmd, c);
            }
            if (chapter["sub-chapters"] != null)
            {
                foreach (dynamic subChapter in chapter["sub-chapters"])
                {
                    ProcessChapter(subChapter, c);
                }
            }
        }

        private static void ProcessCommandData(dynamic cmd, OutputChapter current)
        {
            string name = cmd["name"]["web"];
            Console.WriteLine(name);
            if (cmds.ContainsKey(name))
            {
                new OutputCmdlet(current, name, cmds[name].type.Name, cmds[name].cmdlet);
            }
            else
            {
                new OutputCmdlet(current, name, null, null);
            }
        }

        private static Dictionary<string, APICmdlet> getImplementedCmdlets()
        {
            XDocument doc = XDocument.Parse(File.ReadAllText("psCheckPoint.xml"));

            List<XElement> members = doc.Descendants("members").Descendants("member")
                        .Where(c => c.Descendants("api").Any())
                        .ToList();

            Dictionary<string, APICmdlet> cmds = new Dictionary<string, APICmdlet>();

            foreach (XElement member in members)
            {
                string fullClassName = member.Attribute("name").Value.Split(':').Last();
                Assembly asm = typeof(CheckPointSession).Assembly;
                Type t = asm.GetType(fullClassName);

                foreach (XElement api in member.Descendants("api"))
                {
                    string cmd = api.Attribute("cmd").Value;
                    string cmdlet = api.Value;

                    cmds[cmd] = new APICmdlet(cmd, cmdlet, t);
                }
            }

            return cmds;
        }

        private static Dictionary<string, ExtraCmdlet> getImplementedExtraCmdlets()
        {
            XDocument doc = XDocument.Parse(File.ReadAllText("psCheckPoint.xml"));

            List<XElement> members = doc.Descendants("members").Descendants("member")
                        .Where(c => c.Descendants("extra").Any())
                        .ToList();

            Dictionary<string, ExtraCmdlet> cmds = new Dictionary<string, ExtraCmdlet>();

            foreach (XElement member in members)
            {
                string fullClassName = member.Attribute("name").Value.Split(':').Last();
                Assembly asm = typeof(CheckPointSession).Assembly;
                Type t = asm.GetType(fullClassName);

                foreach (XElement api in member.Descendants("extra"))
                {
                    string category = api.Attribute("category").Value;
                    string cmdlet = api.Value;

                    cmds[category + '/' + cmdlet] = new ExtraCmdlet(category, cmdlet, t);
                }
            }

            return cmds;
        }
    }

    internal class APICmdlet
    {
        internal APICmdlet(string apicmd, string cmdlet, Type type)
        {
            this.apicmd = apicmd;
            this.cmdlet = cmdlet;
            this.type = type;
        }

        internal string apicmd { get; set; }
        internal string cmdlet { get; set; }
        internal Type type { get; set; }
    }

    internal class OutputChapter
    {
        public OutputChapter(string name)
        {
            Name = name;
        }

        public OutputChapter(string name, OutputChapter parent)
        {
            Name = name;
            parent.SubChapters.Add(this);
        }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2)]
        public List<OutputCmdlet> Cmdlets { get; set; } = new List<OutputCmdlet>();

        [JsonProperty(Order = 3)]
        public List<OutputChapter> SubChapters { get; set; } = new List<OutputChapter>();
    }

    internal class OutputCmdlet
    {
        public OutputCmdlet(OutputChapter parent, string name, string @class, string cmdlet)
        {
            Name = name;
            Class = @class;
            Cmdlet = cmdlet;

            parent.Cmdlets.Add(this);
        }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 2)]
        public string Class { get; set; }

        [JsonProperty(Order = 3)]
        public string Cmdlet { get; set; }
    }

    internal class ExtraCmdlet
    {
        internal ExtraCmdlet(string category, string cmdlet, Type type)
        {
            this.category = category;
            this.cmdlet = cmdlet;
            this.type = type;
        }

        internal string category { get; set; }
        internal string cmdlet { get; set; }
        internal Type type { get; set; }
    }
}