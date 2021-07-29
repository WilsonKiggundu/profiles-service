using System;
using Newtonsoft.Json;

namespace ProfileService.Contracts.Zoom
{
    public class Webinar
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }
        
        [JsonProperty("type")]
        public WebinarType Type { get; set; }

        [JsonProperty("start_time")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; } // time in minutes

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("agenda")]
        public string Agenda { get; set; }

        [JsonProperty("recurrence")]
        public WebinarRecurrence Recurrence { get; set; }

        [JsonProperty("settings")]
        public WebinarSettings Settings { get; set; }

    }


    public class WebinarResponse
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

    public class WebinarSettings
    {
        [JsonProperty("host_video")]
        public bool HostVideo { get; set; } = true;
        
        [JsonProperty("panelists_video")]
        public bool PanelistsVideo { get; set; } = true;
        
        [JsonProperty("practice_session")]
        public bool PracticeSession { get; set; } = true;
        
        [JsonProperty("hd_video")]
        public bool HdVideo { get; set; } = true;
        
        [JsonProperty("approval_type")]
        public WebinarApprovalType ApprovalType { get; set; }
        
        [JsonProperty("registration_type")]
        public WebinarRegistrationType RegistrationType { get; set; }
        
        [JsonProperty("audio")]
        public string Audio { get; set; } = "both";
        
        [JsonProperty("audio_recording")]
        public string AudioRecording { get; set; } = "cloud";
        
        [JsonProperty("enforce_login")]
        public bool EnforceLogin { get; set; }
        
        [JsonProperty("alternative_hosts")]
        public string AlternativeHosts { get; set; }
        
        [JsonProperty("close_registration")]
        public bool CloseRegistration { get; set; }
        
        [JsonProperty("show_share_button")]
        public bool ShowShareButton { get; set; }
        
        [JsonProperty("allow_multiple_devices")]
        public bool AllowMultipleDevices { get; set; }
        
        [JsonProperty("on_demand")]
        public bool OnDemand { get; set; }
        
        [JsonProperty("global_dial_in_countries")]
        public string[] GlobalDialInCountries { get; set; }
        
        [JsonProperty("contact_name")]
        public string ContactName { get; set; }
        
        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }
        
        [JsonProperty("registrants_restrict_number")]
        public int RegistrantsRestrictNumber { get; set; } = 0;
        
        [JsonProperty("post_webinar_survey")]
        public bool PostWebinarSurvey { get; set; }
        
        [JsonProperty("survey_url")]
        public string SurveyUrl { get; set; }
        
        [JsonProperty("registrants_email_notification")]
        public bool RegistrantsEmailNotification { get; set; } = true;
        
        [JsonProperty("meeting_authentication")]
        public bool MeetingAuthentication { get; set; }
        
        [JsonProperty("authentication_option")]
        public string AuthenticationOption { get; set; }
        
        [JsonProperty("authentication_domains")]
        public string AuthenticationDomains { get; set; }
        
        [JsonProperty("question_and_answer")]
        public QuestionAndAnswer QuestionAndAnswer { get; set; }
        
        [JsonProperty("email_language")]
        public string EmailLanguage { get; set; } = "en-US";
        
        [JsonProperty("panelists_invitation_email_notification")]
        public bool PanelistsInvitationEmailNotification { get; set; } = true;
        
        [JsonProperty("attendees_and_panelists_reminder_email_notification")]
        public EmailNotification AttendeesAndPanelistsReminderEmailNotification { get; set; }
        
        [JsonProperty("follow_up_attendees_email_notification")]
        public EmailNotification FollowUpAttendeesEmailNotification { get; set; }
        
        [JsonProperty("follow_up_absentees_email_notification")]
        public EmailNotification FollowUpAbsenteesEmailNotification { get; set; }   

    }

    public class EmailNotification
    {
        [JsonProperty("enable")]
        public bool Enable { get; set; } = true;
        
        [JsonProperty("type")]
        public ReminderPlan Type { get; set; }  
    }

    public class QuestionAndAnswer
    {
        [JsonProperty("enable")]
        public bool Enable { get; set; }
        
        [JsonProperty("allow_anonymous_questions")]
        public bool AllowAnonymousQuestions { get; set; }
        
        [JsonProperty("answer_questions")]
        public string AnswerQuestions { get; set; } = "all";
        
        [JsonProperty("attendees_can_upvote")]
        public bool AttendeesCanUpvote { get; set; } = true;
        
        [JsonProperty("attendees_can_comment")]
        public bool AttendeesCanComment { get; set; } = true;
    }

    public class WebinarRecurrence
    {
        [JsonProperty("type")]
        public WebinarRecurrenceType Type { get; set; }
        
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

    public enum ReminderPlan
    {
        NoPlan = 0,
        HourBefore = 1,
        DayBefore = 2,
        HourAndDayBefore = 3,
        WeekBefore = 4,
        HourAndWeekBefore = 5,
        DayAndWeekBefore = 6,
        HourAndDayAndWeekBefore = 7
    }
    
    public enum WebinarRegistrationType
    {
        RegisterOnceAttendAnySession = 1,
        RegisterForEachSession = 2,
        RegisterOnceChooseOneOrMoreSessions = 3
    }
    
    public enum WebinarApprovalType
    {
        Automatic = 0,
        Manual = 1,
        NoRegistration = 2
    }

    public enum WebinarRecurrenceType
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }
    
    public enum WebinarType
    {
        Webinar = 5,
        RecurringWebinarWithNoFixedTime = 6,
        RecurringWebinarWithFixedTime = 9,
    }
}