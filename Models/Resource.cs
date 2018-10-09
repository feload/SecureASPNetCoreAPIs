using Newtonsoft.Json;

namespace SecureASPNetCoreAPIs.Models
{
    public abstract class Resource
    {
        // Order refers to arrangement.
        [JsonProperty(Order = -2)]
        public string Href { get; set; }
    }
}