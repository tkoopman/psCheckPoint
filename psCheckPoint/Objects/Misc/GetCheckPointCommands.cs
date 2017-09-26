using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace psCheckPoint.Objects.Misc
{
    /// <api cmd="show-commands">Get-CheckPointCommands</api>
    /// <summary>
    /// <para type="synopsis"></para>
    /// <para type="description"></para>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CheckPointCommands")]
    public class GetCheckPointCommands : CheckPointCmdlet<CommandsResult>
    {
        public override string Command { get { return "show-commands"; } }
    }

    public class CommandResult
    {
        [JsonConstructor]
        private CommandResult(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; private set; }
    }

    [JsonObject]
    public class CommandsResult : IEnumerable<CommandResult>
    {
        [JsonConstructor]
        private CommandsResult(List<CommandResult> commands)
        {
            Commands = commands;
        }

        [JsonProperty(PropertyName = "commands")]
        public List<CommandResult> Commands { get; private set; }

        public IEnumerator<CommandResult> GetEnumerator()
        {
            return ((IEnumerable<CommandResult>)Commands).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CommandResult>)Commands).GetEnumerator();
        }
    }
}