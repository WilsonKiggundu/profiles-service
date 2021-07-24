using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfileService.Contracts
{
    public class EventContract
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("startDateTime")]
        public DateTimeOffset StartDateTime { get; set; }
        
        [JsonProperty("endDateTime")]
        public DateTimeOffset EndDateTime { get; set; }
        
        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("interval")]
        public int Interval { get; set; }
        
        [JsonProperty("frequency")]
        public int Frequency { get; set; }
        
        [JsonProperty("days")]
        public int[] Days { get; set; }
        
        [JsonProperty("details")]
        public string Details { get; set; }
        
        [JsonProperty("conferenceUrl")]
        public string ConferenceUrl { get; set; }
        
        [JsonProperty("createdBy")]
        public Guid CreatedBy { get; set; }
        
        [JsonProperty("uploads")]
        public List<EventUpload> Uploads { get; set; }
        
        [JsonProperty("featured")]
        public bool Featured { get; set; }

        public bool IsZoomEvent { get; set; }

        public EventContract()
        {
            IsZoomEvent = Location.ToLower().Equals("zoom") && Type.ToLower().Equals("Webinar");
        }
    }

    public class EventUpload
    {
        [JsonProperty("ownerId")]
        public Guid? OwnerId { get; set; }
        
        [JsonProperty("entityId")]
        public string EntityId { get; set; }
        
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        
        [JsonProperty("fileSize")]
        public string FileSize { get; set; }
        
        [JsonProperty("path")]
        public string Path { get; set; }
        
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
        
        [JsonProperty("dateCreated")]
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    }
    
    public class EventSearch    
    {
        public int? Id { get; set; }
        public DateTimeOffset? StartDateTime { get; set; }
    }
}