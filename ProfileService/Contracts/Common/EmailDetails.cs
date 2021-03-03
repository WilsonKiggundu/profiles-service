using System;

namespace ProfileService.Contracts.Common
{
    public class EmailDetails
    {
        public Guid PersonId { get; set; }
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public EmailTemplate Template { get; set; }
    }

    public enum EmailTemplate
    {
        IfYourProfileIsIncomplete = 1,
        WhenACommentIsAddedOnYourPost = 2,
        WhenYouAreFollowed = 3,
        WhenYouAreAddedToAStartup = 4,
        WhenThereAreSystemUpdates = 5,
        WhenPeopleYouFollowPostSomething = 6,
        WhenTheStartupYouFollowUpdatesProfile = 7,
        WhenAnEvenIsPosted = 8,
        WhenAJobIsPosted = 9
    }
}