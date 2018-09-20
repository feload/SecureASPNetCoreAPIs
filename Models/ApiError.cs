using System.ComponentModel;
using Newtonsoft.Json;

namespace SecureASPNetCoreAPIs.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string SackTrace { get; set; }
    }
}