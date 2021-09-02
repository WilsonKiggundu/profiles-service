using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfileService.Contracts.Zoom
{
    public class Meeting
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        
        [JsonProperty("type")]
        public MeetingType Type { get; set; }
        
        [JsonProperty("pre_schedule")]

        public bool PreSchedule { get; set; }

        [JsonProperty("start_time")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; } // time in minutes

        [JsonProperty("scheduled_for")]
        public string ScheduledFor { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("agenda")]
        public string Agenda { get; set; }

        [JsonProperty("recurrence")]
        public WebinarRecurrence Recurrence { get; set; }

        [JsonProperty("settings")]
        public MeetingSettings Settings { get; set; }
        
        [JsonProperty("template_id")] public string TemplateId { get; set; }

    }
    
    public class MeetingSettings
    {
        [JsonProperty("host_video")] public bool HostVideo { get; set; } = true;
        [JsonProperty("participant_video")] public bool ParticipantVideo { get; set; } = true;
        [JsonProperty("join_before_host")] public bool JoinBeforeHost { get; set; } = true;
        [JsonProperty("jbh_time")] public int JoinBeforeHostTime { get; set; } = 5;
        [JsonProperty("mute_upon_entry")] public bool MuteUponEntry { get; set; } = true;
        [JsonProperty("watermark")] public bool Watermark { get; set; } = false;
        [JsonProperty("use_pmi")] public bool UsePersonalMeetingId { get; set; } = false;
        [JsonProperty("approval_type")]public int ApprovalType { get; set; } = 0;
        [JsonProperty("registration_type")] public int RegistrationType { get; set; } = 3;
        [JsonProperty("audio")] public string Audio { get; set; } = "both";
        [JsonProperty("audio_recording")] public string AudioRecording { get; set; } = "cloud";
        [JsonProperty("alternative_hosts")] public string AlternativeHosts { get; set; }
        [JsonProperty("close_registration")] public bool CloseRegistration { get; set; } = true;
        [JsonProperty("waiting_room")] public bool WaitingRoom { get; set; } = false;
        [JsonProperty("global_dial_in_countries")] public string[] GlobalDialInCountries { get; set; }
        [JsonProperty("contact_name")] public string ContactName { get; set; }
        [JsonProperty("contact_email")] public string ContactEmail { get; set; }
        [JsonProperty("registrants_email_notification")] public bool RegistrantsEmailNotification { get; set; } = true;
        [JsonProperty("registrants_confirmation_email")] public bool RegistrantsConfirmationEmail { get; set; } = true;
        [JsonProperty("meeting_authentication")] public bool MeetingAuthentication { get; set; } = true;
        [JsonProperty("authentication_option")] public string AuthenticationOption { get; set; }
        [JsonProperty("authentication_domains")] public string AuthenticationDomains { get; set; }
        [JsonProperty("authentication_exception")] public List<NameEmailObject> AuthenticationException { get; set; }
        [JsonProperty("additional_data_center_regions")] public string[] EmailLanguage { get; set; }
        [JsonProperty("breakout_room")] public BreakoutRoom BreakoutRoom { get; set; }
        [JsonProperty("show_share_button")] public bool ShowShareButton { get; set; }
        [JsonProperty("allow_multiple_devices")] public bool AllowMultipleDevices { get; set; }
        [JsonProperty("language_interpretation")] public LanguageInterpretation LanguageInterpretation { get; set; }
        [JsonProperty("encryption_type")] public string EncryptionType { get; set; } = "enhanced_encryption";
        [JsonProperty("alternative_hosts_email_notification")]
        public bool AltHostsEmailNotif { get; set; } = true;
        [JsonProperty("approved_or_denied_countries_or_regions")] public object ApprovedOrDeniedCountries { get; set; }
    }

    public class LanguageInterpretation
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = false;

        [JsonProperty("interpreters")]
        public List<Interpreter> Interpreters { get; set; }
        
    }

    public class Interpreter
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("languages")]
        public string Languages { get; set; }
    }
    
    public class Room
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("participants")]
        public List<string> Participants { get; set; }
    }
    
    public class BreakoutRoom   
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;
        
        [JsonProperty("rooms")]
        public List<Room> Rooms { get; set; }
    }
    
    public class NameEmailObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }


    public class MeetingResponse
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("host_id")]
        public string HostId { get; set; }
        
        [JsonProperty("host_email")]
        public string HostEmail { get; set; }
        
        [JsonProperty("start_url")]
        public string StartUrl { get; set; }
        
        [JsonProperty("join_url")]
        public string JoinUrl { get; set; }
    }   

    public class MeetingDetails : Meeting
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("host_id")]
        public string HostId { get; set; }
        
        [JsonProperty("host_email")]
        public string HostEmail { get; set; }
        
        [JsonProperty("start_url")]
        public string StartUrl { get; set; }
        
        [JsonProperty("join_url")]
        public string JoinUrl { get; set; }
    }
    

    public class MeetingRecurrence  
    {
        [JsonProperty("type")]
        public MeetingRecurrenceType Type { get; set; }
        
        [JsonProperty("repeat_interval")]
        public int RepeatInterval { get; set; }

        /*
         * 1 - Sunday
         * ...
         * 7 - Saturday
         */
        [JsonProperty("weekly_days")]
        public string WeeklyDays { get; set; } // used for Type = 2

        [JsonProperty("monthly_day")]
        public int MonthlyDay { get; set; } // used for Type = 3

        /*
         * -1 - Last week of month
         * 1 - First week
         * 2 - Second week
         * 3 - Third week
         * 4 - Fourth week
         */
        [JsonProperty("monthly_week")]  
        public int MonthlyWeek { get; set; }
        
        [JsonProperty("monthly_week_day")]  
        public int MonthlyWeekDay { get; set; }

        [JsonProperty("end_times")]
        public int EndTimes { get; set; } // how many times it would recur before canceled. [1-365]

        // can't be used with end_times
        // should be in UTC time
        [JsonProperty("end_date_time")]
        public string EndDateTime { get; set; } 
    }

    public enum MeetingRecurrenceType   
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }
    
    public enum MeetingType
    {
        Instant = 1,
        Scheduled = 2,
        RecurringNoFixedTime = 3,
        RecurringFixedTime = 8,
    }
}