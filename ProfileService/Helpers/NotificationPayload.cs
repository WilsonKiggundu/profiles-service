using Newtonsoft.Json;

namespace ProfileService.Helpers
{
    public class NotificationPayload
    {
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