using Newtonsoft.Json;

namespace ProfileService.Contracts.Zoom
{
    public class WebinarPanelist
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}