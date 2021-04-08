using Newtonsoft.Json;

namespace ProfileService.Helpers
{
    public class NotificationAction
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}