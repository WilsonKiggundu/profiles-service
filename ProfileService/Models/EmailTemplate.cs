using ProfileService.Models.Common;

namespace ProfileService.Models
{
    public class EmailTemplate : BaseModel
    {
        public EmailType Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public enum EmailType
    {
        Welcome = 1,
        UpdateProfile = 2,
        JobPosted = 3,
        JobApplication = 4,
        EventAdded = 5,
        EventAttendance = 6,
        ProfileConnection = 7,
        ProfileCreation = 8
    }
}