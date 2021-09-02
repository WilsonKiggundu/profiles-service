using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfileService.Contracts.Zoom
{
    public class Registrant
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("zip")]
        public string Zip { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("industry")]
        public string Industry { get; set; }
        
        [JsonProperty("org")]
        public string Org { get; set; }
        
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }
        
        [JsonProperty("purchasing_time_frame")]
        public string PurchasingTimeFrame { get; set; }
        
        [JsonProperty("role_in_purchase_process")]
        public string RoleInPurchaseProcess { get; set; }
        
        [JsonProperty("number_of_employees")]
        public string NumberOfEmployees { get; set; }
        
        [JsonProperty("comments")]
        public string Comments { get; set; }
        
        [JsonProperty("custom_questions")]
        public List<CustomQuestion> CustomQuestions { get; set; }
    }

    public class CustomQuestion
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Participant
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("user_email")]
        public string UserEmail { get; set; }
    }

    public class ParticipantsResponse
    {
        [JsonProperty("participants")]
        public ICollection<Participant> Participants { get; set; }
        
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
        
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty("total_records")]
        public int TotalRecords { get; set; }
        
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }
    }

    public class RegistrantsResponse
    {
        [JsonProperty("registrants")]
        public ICollection<Registrant> Registrants { get; set; }
        
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
        
        [JsonProperty("page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty("total_records")]
        public int TotalRecords { get; set; }
        
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }
    }

    public class RegistrationResponse
    {
        [JsonProperty("registrant_id")]
        public string RegistrantId { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("topic")]
        public string Topic { get; set; }
        
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        
        [JsonProperty("join_url")]
        public string JoinUrl { get; set; }
    }
}