using Newtonsoft.Json;
using System;
using System.Management.Automation;

namespace psCheckPoint
{
    internal class ValidateColorAttribute : ValidateArgumentsAttribute
    {
        private static string[] ValidColors =
        {
            "aquamarine 1",
            "black",
            "blue",
            "blue 1",
            "burly wood 4",
            "cyan",
            "dark green",
            "dark khaki",
            "dark orchid",
            "dark orange 3",
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
            "yellow"
        };

        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            if (arguments != null)
            {
                string value = arguments.ToString().ToLower();
                if (!(string.IsNullOrWhiteSpace(value) || (Array.IndexOf(ValidColors, value) > -1)))
                {
                    throw new ValidationMetadataException($"{value} is not a valid color.");
                }
            }
        }
    }

    public abstract class CheckPointColorCmdlet<T> : CheckPointCmdlet<T>
    {
        [JsonProperty(PropertyName = "color", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Colour")]
        [ValidateColor]
        public string Color
        {
            get { return _color; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _color = null;
                }
                else
                {
                    _color = value;
                }
            }
        }

        private string _color;
    }
}