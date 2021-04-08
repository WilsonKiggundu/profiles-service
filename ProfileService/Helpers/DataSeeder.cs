using System.Linq;
using ProfileService.Models.Common;
using ProfileService.Models.Preferences;
using ProfileService.Repositories;
using WebPush;

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

        public static void GenerateVapidKeys(ProfileServiceContext context)
        {
            if (context.VapidKeys.Any()) return;
            var keys = VapidHelper.GenerateVapidKeys();

            context.VapidKeys.Add(new VapidKeys
            {
                PublicKey = keys.PublicKey,
                PrivateKey = keys.PrivateKey,
                Subject = keys.Subject
            });

            context.SaveChanges();
        }
    }
}