using System.Linq;
using ProfileService.Models.Preferences;
using ProfileService.Repositories;

namespace ProfileService.Helpers
{
    public static class DataSeeder
    {
        public static void SeedEmailPreferences(ProfileServiceContext context)
        {
            if (context.EmailPreferences.Any()) return;
            
            var settings = context.Persons.Select(s => new EmailSettings{PersonId = s.Id}).ToList();
            context.EmailPreferences.AddRange(settings);
            context.SaveChanges();
        }
    }
}