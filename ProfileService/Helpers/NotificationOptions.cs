using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfileService.Helpers
{
    public class NotificationOptions
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }
        
        [JsonProperty("tag")]
        public string Tag { get; set; }
        
        [JsonProperty("actions")]
        public List<NotificationAction> Actions { get; set; }
    }
}