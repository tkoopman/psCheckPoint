using Newtonsoft.Json;

namespace psCheckPointIA
{
    internal class Responses<T>
    {
        [JsonProperty(PropertyName = "responses")]
        public T[] responses { get; set; }
    }
}