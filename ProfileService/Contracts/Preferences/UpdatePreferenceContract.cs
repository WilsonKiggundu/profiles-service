using System;
using Newtonsoft.Json;

namespace ProfileService.Contracts.Preferences
{
    public class UpdatePreferenceContract
    {
        public Guid PersonId { get; set; }
        public PreferenceType Type { get; set; }
        public bool Value { get; set; }
    }

    public enum PreferenceType
    {
        ProfileIsIncomplete = 1,
        CommentIsAddedOnYourPost = 2,
        YouAreFollowed = 3,
        YouAreAddedToAStartup = 4,
        ThereAreSystemUpdates = 5,
        PeopleYouFollowPostSomething = 6,
        TheStartupYouFollowUpdatesProfile = 7,
        EventIsPosted = 8,
        EventReminders = 9,
        JobIsPosted = 10,
        WeeklyBlogDigest = 11,
        ApplyForJobReminder = 12,
        JobAppliedForReminder = 13,
        ArticleIsPosted = 14,
        YourPostIsLiked = 15
    }
}