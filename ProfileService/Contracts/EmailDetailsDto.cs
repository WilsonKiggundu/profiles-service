using Newtonsoft.Json;

namespace ProfileService.Contracts
{
    public class EmailDetailsDto
    {
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("recipient")]
        public string Recipient { get; set; }
        [JsonProperty("senderName")]
        public string SenderName { get; set; } = "MyVillage";
        [JsonProperty("senderEmail")]
        public string SenderEmail { get; set; } = "myvillage@devops.innovationvillage.co.ug";
        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}