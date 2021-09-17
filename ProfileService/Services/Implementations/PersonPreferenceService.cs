using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Preferences;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PersonPreferenceService : IPersonPreferenceService
    {
        private readonly IPersonPreferencesRepository _preferencesRepository;
        private readonly ILogger<PersonPreferenceService> _logger;

        public PersonPreferenceService(IPersonPreferencesRepository preferencesRepository, ILogger<PersonPreferenceService> logger)
        {
            _preferencesRepository = preferencesRepository;
            _logger = logger;
        }

        public async Task<EmailSettingsContract> GetByIdAsync(Guid id)
        {
            await EnsureInitialized(id);
            
            var settings = await _preferencesRepository.GetByIdAsync(id);
            
            return new EmailSettingsContract
            {
                EventReminders = settings.EventReminders,
                EventIsPosted = settings.EventIsPosted,
                JobIsPosted = settings.JobIsPosted,
                ProfileIsIncomplete = settings.ProfileIsIncomplete,
                WeeklyBlogDigest = settings.WeeklyBlogDigest,
                YouAreFollowed = settings.YouAreFollowed,
                ThereAreSystemUpdates = settings.ThereAreSystemUpdates,
                PeopleYouFollowPostSomething = settings.PeopleYouFollowPostSomething,
                CommentIsAddedOnYourPost = settings.CommentIsAddedOnYourPost,
                TheStartupYouFollowUpdatesProfile = settings.TheStartupYouFollowUpdatesProfile,
                YouAreAddedToAStartup = settings.YouAreAddedToAStartup,
                ArticleIsPosted = settings.ArticleIsPosted,
                ApplyForJobReminder = settings.ApplyForJobReminder,
                JobAppliedForReminder = settings.JobAppliedForReminder,
                YourPostIsLiked = settings.YourPostIsLiked
            };
        }

        public async Task UpdateAsync(UpdatePreferenceContract preference)
        {
            await EnsureInitialized(preference.PersonId);

            var settings = await _preferencesRepository.GetByIdAsync(preference.PersonId);

            switch (preference.Type)
            {
                case PreferenceType.ProfileIsIncomplete:
                    settings.ProfileIsIncomplete = preference.Value;
                    break;
                case PreferenceType.CommentIsAddedOnYourPost:
                    settings.CommentIsAddedOnYourPost = preference.Value;
                    break;
                case PreferenceType.YouAreFollowed:
                    settings.YouAreFollowed = preference.Value;
                    break;
                case PreferenceType.YouAreAddedToAStartup:
                    settings.YouAreAddedToAStartup = preference.Value;
                    break;
                case PreferenceType.ThereAreSystemUpdates:
                    settings.ThereAreSystemUpdates = preference.Value;
                    break;
                case PreferenceType.PeopleYouFollowPostSomething:
                    settings.PeopleYouFollowPostSomething = preference.Value;
                    break;
                case PreferenceType.TheStartupYouFollowUpdatesProfile:
                    settings.TheStartupYouFollowUpdatesProfile = preference.Value;
                    break;
                case PreferenceType.EventIsPosted:
                    settings.EventIsPosted = preference.Value;
                    break;
                case PreferenceType.EventReminders:
                    settings.EventReminders = preference.Value;
                    break;
                case PreferenceType.JobIsPosted:
                    settings.JobIsPosted = preference.Value;
                    break;
                case PreferenceType.WeeklyBlogDigest:
                    settings.WeeklyBlogDigest = preference.Value;
                    break;
                case PreferenceType.YourPostIsLiked:
                    settings.YourPostIsLiked = preference.Value;
                    break;
                case PreferenceType.ApplyForJobReminder:
                    settings.ApplyForJobReminder = preference.Value;
                    break;
                case PreferenceType.JobAppliedForReminder:
                    settings.JobAppliedForReminder = preference.Value;
                    break;
                case PreferenceType.ArticleIsPosted:
                    settings.ArticleIsPosted = preference.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            await _preferencesRepository.UpdateAsync(settings);
        }

        private async Task EnsureInitialized(Guid id)
        {
            var exists = await _preferencesRepository.ExistsAsync(id);

            if (exists is false)
            {
                await _preferencesRepository.InsertAsync(new EmailSettings
                {
                    PersonId = id,
                    EventReminders = true,
                    ArticleIsPosted = true,
                    EventIsPosted = true,
                    JobIsPosted = true,
                    ProfileIsIncomplete = true,
                    WeeklyBlogDigest = true,
                    YouAreFollowed = true,
                    ApplyForJobReminder = true,
                    JobAppliedForReminder = true,
                    ThereAreSystemUpdates = true,
                    YourPostIsLiked = true,
                    PeopleYouFollowPostSomething = true,
                    CommentIsAddedOnYourPost = true,
                    TheStartupYouFollowUpdatesProfile = true,
                    YouAreAddedToAStartup = true
                });
            }
        }
    }
}