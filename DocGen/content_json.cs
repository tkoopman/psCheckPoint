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
    internal class APICmdlet : IComparable
    {
        #region Constructors

        internal APICmdlet(string apiCmd, string cmdlet, Type type)
        {
            ApiCmd = apiCmd;
            Cmdlet = cmdlet;
            Type = type;
        }

        #endregion Constructors

        #region Properties

        internal string ApiCmd { get; set; }
        internal string Cmdlet { get; set; }
        internal Type Type { get; set; }

        #endregion Properties

        #region Methods

        public int CompareTo(object obj) => Cmdlet.CompareTo((obj as APICmdlet).Cmdlet);

        #endregion Methods
    }

    internal class ContentJson
    {
        #region Fields

        private static Dictionary<string, List<APICmdlet>> cmdExtras;
        private static Dictionary<string, APICmdlet> cmds;

        #endregion Fields

        #region Methods

        private static void AppendIAContentJson(OutputChapter output)
        {
            var iaCmds = GetImplementedIACmdlets();

            var list = iaCmds.Keys.ToList();
            list.Sort();

            var current = new OutputChapter("Identity Awareness", output);
            foreach (string i in list)
            {
                Console.WriteLine(iaCmds[i].Cmdlet);
                new OutputCmdlet(current, iaCmds[i].ApiCmd, iaCmds[i].Type.Name, iaCmds[i].Cmdlet);
            }
        }

        private static void CreateContentJson(string path)
        {
            GetImplementedCmdlets();
            var output = new OutputChapter("chapters");

            // Get List of all Check Point API calls
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = client.GetAsync($"https://sc1.checkpoint.com/documents/latest/APIs/data/v1.1/dynamic/content.json").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string strJson = response.Content.ReadAsStringAsync().Result;

                        dynamic content = JsonConvert.DeserializeObject(strJson);

                        foreach (var chapter in content["chapters"])
                            ProcessChapter(chapter, output);
                    }
                    else
                        Console.WriteLine("Error getting content.json");
                }
            }

            // Remove empty chapters
            foreach (var chapter in output.SubChapters.ToList())
                if (chapter.SubChapters.Count == 0 && chapter.Cmdlets.Count == 0)
                    output.SubChapters.Remove(chapter);

            AppendIAContentJson(output);

            // Output results to file
            File.WriteAllText($@"{path}\content.json", JsonConvert.SerializeObject(output.SubChapters, Formatting.Indented));
        }

        private static void CreateExtraContentJson(string path)
        {
            var extraCmds = GetImplementedExtraCmdlets();
            var output = new OutputChapter("chapters");

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
                new OutputCmdlet(current, extraCmds[i].Cmdlet, extraCmds[i].Type.Name, extraCmds[i].Cmdlet);
            }

            // Output results to file
            File.WriteAllText($@"{path}\extra-content.json", JsonConvert.SerializeObject(output.SubChapters, Formatting.Indented));
        }

        private static void GetImplementedCmdlets()
        {
            var doc = XDocument.Parse(File.ReadAllText("psCheckPoint.xml"));

            var members = doc.Descendants("members").Descendants("member")
                        .Where(c => c.Descendants("api").Any())
                        .ToList();

            cmds = new Dictionary<string, APICmdlet>();
            cmdExtras = new Dictionary<string, List<APICmdlet>>();

            foreach (var member in members)
            {
                string fullClassName = member.Attribute("name").Value.Split(':').Last();
                var asm = typeof(OpenCheckPointSession).Assembly;
                var t = asm.GetType(fullClassName);

                foreach (var api in member.Descendants("api"))
                {
                    string cmd = api.Attribute("cmd")?.Value;
                    string parent = api.Attribute("parent")?.Value;
                    string cmdlet = api.Value;

                    if (cmd != null)
                    {
                        cmds[cmd] = new APICmdlet(cmd, cmdlet, t);
                    }
                    else if (parent != null)
                    {
                        if (cmdExtras.ContainsKey(parent))
                        {
                            cmdExtras[parent].Add(new APICmdlet(cmdlet, cmdlet, t));
                        }
                        else
                        {
                            cmdExtras[parent] = new List<APICmdlet>() {
                                new APICmdlet(cmdlet, cmdlet, t)
                            };
                        }
                    }
                }
            }
        }

        private static Dictionary<string, ExtraCmdlet> GetImplementedExtraCmdlets()
        {
            var doc = XDocument.Parse(File.ReadAllText("psCheckPoint.xml"));

            var members = doc.Descendants("members").Descendants("member")
                        .Where(c => c.Descendants("extra").Any())
                        .ToList();

            var cmds = new Dictionary<string, ExtraCmdlet>();

            foreach (var member in members)
            {
                string fullClassName = member.Attribute("name").Value.Split(':').Last();
                var asm = typeof(OpenCheckPointSession).Assembly;
                var t = asm.GetType(fullClassName);

                foreach (var api in member.Descendants("extra"))
                {
                    string category = api.Attribute("category").Value;
                    string cmdlet = api.Value;

                    cmds[category + '/' + cmdlet] = new ExtraCmdlet(category, cmdlet, t);
                }
            }

            return cmds;
        }

        private static Dictionary<string, APICmdlet> GetImplementedIACmdlets()
        {
            var doc = XDocument.Parse(File.ReadAllText("psCheckPoint.xml"));

            var members = doc.Descendants("members").Descendants("member")
                        .Where(c => c.Descendants("IA").Any())
                        .ToList();

            var cmds = new Dictionary<string, APICmdlet>();

            foreach (var member in members)
            {
                string fullClassName = member.Attribute("name").Value.Split(':').Last();
                var asm = typeof(OpenCheckPointSession).Assembly;
                var t = asm.GetType(fullClassName);

                foreach (var api in member.Descendants("IA"))
                {
                    string cmd = api.Attribute("cmd").Value;
                    string cmdlet = api.Value;

                    cmds[cmd] = new APICmdlet(cmd, cmdlet, t);
                }
            }

            return cmds;
        }

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

        private static void ProcessChapter(dynamic chapter, OutputChapter current)
        {
            Console.WriteLine(chapter["name"]);
            var c = new OutputChapter(chapter["name"].ToString(), current);

            foreach (var cmd in chapter["commands-data"])
                ProcessCommandData(cmd, c);

            if (cmdExtras.ContainsKey((string)chapter["name"]))
            {
                var list = cmdExtras[(string)chapter["name"]];
                list.Sort();
                foreach (var cmd in list)
                    new OutputCmdlet(c, cmd.Cmdlet, cmd.Type.Name, cmd.Cmdlet);
            }

            if (chapter["sub-chapters"] != null)
                foreach (var subChapter in chapter["sub-chapters"])
                    ProcessChapter(subChapter, c);
        }

        private static void ProcessCommandData(dynamic cmd, OutputChapter current)
        {
            string name = cmd["name"]["web"];
            Console.WriteLine(name);
            if (cmds.ContainsKey(name))
                new OutputCmdlet(current, name, cmds[name].Type.Name, cmds[name].Cmdlet);
            else
                new OutputCmdlet(current, name, null, null);
        }

        #endregion Methods
    }

    internal class ExtraCmdlet
    {
        #region Constructors

        internal ExtraCmdlet(string category, string cmdlet, Type type)
        {
            Category = category;
            Cmdlet = cmdlet;
            Type = type;
        }

        #endregion Constructors

        #region Properties

        internal string Category { get; set; }
        internal string Cmdlet { get; set; }
        internal Type Type { get; set; }

        #endregion Properties
    }

    internal class OutputChapter
    {
        #region Constructors

        public OutputChapter(string name)
        {
            Name = name;
        }

        public OutputChapter(string name, OutputChapter parent)
        {
            Name = name;
            parent.SubChapters.Add(this);
        }

        #endregion Constructors

        #region Properties

        [JsonProperty(Order = 2)]
        public List<OutputCmdlet> Cmdlets { get; set; } = new List<OutputCmdlet>();

        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        [JsonProperty(Order = 3)]
        public List<OutputChapter> SubChapters { get; set; } = new List<OutputChapter>();

        #endregion Properties
    }

    internal class OutputCmdlet
    {
        #region Constructors

        public OutputCmdlet(OutputChapter parent, string name, string @class, string cmdlet)
        {
            Name = name;
            Class = @class;
            Cmdlet = cmdlet;

            parent.Cmdlets.Add(this);
        }

        #endregion Constructors

        #region Properties

        [JsonProperty(Order = 2)]
        public string Class { get; set; }

        [JsonProperty(Order = 3)]
        public string Cmdlet { get; set; }

        [JsonProperty(Order = 1)]
        public string Name { get; set; }

        #endregion Properties
    }
}