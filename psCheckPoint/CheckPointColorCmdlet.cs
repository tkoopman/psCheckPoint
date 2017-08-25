using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint
{
    public abstract class CheckPointColorCmdlet<T> : CheckPointCmdlet<T>
    {
        [JsonProperty(PropertyName = "color", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Colour")]
        [ValidateSet(
            "aquamarine 1",
            "black",
            "blue",
            "blue 1",
            "burly wood 4",
            "cyan",
            "dark green", "dark khaki", "dark orchid", "dark orange 3",
            "dark sea green 3",
            "deep pink",
            "deep sky blue 1",
            "dodger blue 3",
            "firebrick",
            "foreground",
            "forest green",
            "gold",
            "gold 3",
            "gray 83",
            "gray 90",
            "green",
            "lemon chiffon",
            "light coral",
            "light sea green",
            "light sky blue 4",
            "magenta",
            "medium orchid",
            "medium slate blue",
            "medium violet red",
            "navy blue",
            "olive drab",
            "orange",
            "red",
            "sienna",
            "yellow",
            "",
            null,
            IgnoreCase = true
            )
        ]
        public string Color { get; set; }
    }
}