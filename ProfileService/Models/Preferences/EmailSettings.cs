using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Preferences
{
    public class EmailSettings
    {    
        [Key] public Guid PersonId { get; set; }    
        public bool IfYourProfileIsIncomplete { get; set; } = true;
        public bool WhenACommentIsAddedOnYourPost { get; set; } = true;
        public bool WhenYouAreFollowed { get; set; } = true;
        public bool WhenYouAreAddedToAStartup { get; set; } = true;    
        public bool WhenThereAreSystemUpdates { get; set; } = true;
        public bool WhenPeopleYouFollowPostSomething { get; set; } = true;
        public bool WhenTheStartupYouFollowUpdatesProfile { get; set; } = true;
        public bool WhenAnEvenIsPosted { get; set; } = true;
        public bool WhenAJobIsPosted { get; set; } = true;
    }
}