namespace ProfileService.Contracts.Preferences
{
    public class EmailSettingsContract
    {
        public bool ProfileIsIncomplete { get; set; }
        public bool CommentIsAddedOnYourPost { get; set; }
        public bool YourPostIsLiked { get; set; }   
        public bool YouAreFollowed { get; set; }
        public bool PeopleYouFollowPostSomething { get; set; }
        
        // startups
        public bool YouAreAddedToAStartup { get; set; }    
        public bool TheStartupYouFollowUpdatesProfile { get; set; }
        
        // events
        public bool EventIsPosted { get; set; }
        public bool EventReminders { get; set; }  
        
        // jobs
        public bool JobIsPosted { get; set; }
        public bool ApplyForJobReminder { get; set; }
        public bool JobAppliedForReminder { get; set; } 
        
        // articles
        public bool ArticleIsPosted { get; set; }
        public bool WeeklyBlogDigest { get; set; }
        
        // updates
        public bool ThereAreSystemUpdates { get; set; }
    }
}