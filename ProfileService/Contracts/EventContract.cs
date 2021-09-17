using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProfileService.Contracts.Zoom;

namespace ProfileService.Contracts
{
    public enum EventType
    {
        Webinar = 1,
        Meeting = 2
    }
    
    public class EventContract
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("startDateTime")] public string StartDateTime { get; set; }
        [JsonProperty("endDateTime")] public string EndDateTime { get; set; }
        [JsonProperty("tivAffiliation")] public bool TivAffiliation { get; set; }

        [JsonProperty("category")] public string Category { get; set; }
        [JsonProperty("objective")] public string Objective { get; set; }
        [JsonProperty("region")] public string Region { get; set; }
        [JsonProperty("partner")] public string Partner { get; set; }
        [JsonProperty("sector")] public string Sector { get; set; }
        [JsonProperty("location")] public string Location { get; set; } = "zoom";
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("webinarId")] public string WebinarId { get; set; }
        [JsonProperty("id")] public int? Id { get; set; }
        [JsonProperty("frequency")] public string Frequency { get; set; }
        [JsonProperty("interval")] public string Interval { get; set; }
        
        [JsonProperty("is_recurring")] public bool IsRecurring { get; set; }

        // [JsonProperty("days")] public int[] Days { get; set; }
        [JsonProperty("details")] public string Details { get; set; }
        [JsonProperty("conferenceUrl")] public string ConferenceUrl { get; set; }
        [JsonProperty("createdBy")] public Guid? CreatedBy { get; set; }
        [JsonProperty("uploads")] public List<EventUpload> Uploads { get; set; }
        
        [JsonProperty("challengesFaced")] public string ChallengesFaced { get; set; }
        [JsonProperty("lessonsLearnt")] public string LessonsLearnt { get; set; }
        [JsonProperty("achievements")] public string Achievements { get; set; }
        
        [JsonProperty("webinar")] public WebinarDetails Webinar { get; set; }
        [JsonProperty("meeting")] public MeetingDetails Meeting { get; set; }   
        [JsonProperty("attendances")] public List<EventAttendance> Attendances { get; set; }

        [JsonProperty("custom_questions")]
        public string CustomQuestions { get; set; }
    }

    public class EventAttendance
    {
        [JsonProperty("profileId")] public Guid ProfileId { get; set; }
        [JsonProperty("category")] public string Category { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
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