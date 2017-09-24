using Newtonsoft.Json;

namespace psCheckPoint.IA
{
    internal class Responses<T>
    {
        [JsonProperty(PropertyName = "responses")]
        public T[] responses { get; set; }
    }
}