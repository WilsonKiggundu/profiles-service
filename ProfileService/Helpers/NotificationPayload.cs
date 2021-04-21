using Newtonsoft.Json;

namespace ProfileService.Helpers
{
    public class NotificationPayload
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
        
        [JsonProperty("options")]
        public NotificationOptions Options { get; set; }
        
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("requireInteraction")] public bool RequireInteraction { get; set; } = true;
    }
}