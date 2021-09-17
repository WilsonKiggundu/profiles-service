using System;
using System.ComponentModel.DataAnnotations;
using ProfileService.Models.Common;

namespace ProfileService.Models.Preferences
{
    public class EmailSettings : BaseModel
    {    
        public Guid PersonId { get; set; }    
        public bool ProfileIsIncomplete { get; set; } = true;
        public bool CommentIsAddedOnYourPost { get; set; } = true;
        public bool YouAreFollowed { get; set; } = true;
        public bool YouAreAddedToAStartup { get; set; } = true;    
        public bool ThereAreSystemUpdates { get; set; } = true;
        public bool PeopleYouFollowPostSomething { get; set; } = true;
        public bool TheStartupYouFollowUpdatesProfile { get; set; } = true;
        public bool EventIsPosted { get; set; } = true;
        public bool EventReminders { get; set; } = true;
        public bool JobIsPosted { get; set; } = true;
        public bool WeeklyBlogDigest { get; set; } = true;

        public bool YourPostIsLiked { get; set; } = true;
        public bool ApplyForJobReminder { get; set; } = true;
        public bool JobAppliedForReminder { get; set; } = true;
        public bool ArticleIsPosted { get; set; } = true;
    }
}