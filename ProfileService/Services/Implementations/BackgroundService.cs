using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Models;
using ProfileService.Models.Person;
using ProfileService.Repositories;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ProfileServiceContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FreelanceProjectService> _logger;

        public BackgroundService(ProfileServiceContext context, IConfiguration configuration,
            IWebHostEnvironment environment, ILogger<FreelanceProjectService> logger)
        {
            _context = context;
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public async Task SendProfileUpdateRemindersAsync()
        {
            _logger.LogInformation("Getting incomplete profiles...");
            var count = await _context.Persons.CountAsync();
            const int batchSize = 10;
            var batchCount = count < batchSize ? 1 : Math.Ceiling(Convert.ToDecimal(count / batchSize));

            for (var i = 0; i < batchCount; i++)
            {
                var profiles = await _context.Persons
                    .Where(p => p.Email != null)
                    .Include(p => p.Categories)
                    .ThenInclude(c => c.Category)
                    .Include(p => p.Awards)
                    .Include(p => p.Contacts)
                    .Include(p => p.Employment)
                    .Include(p => p.Interests)
                    .Include(p => p.Projects)
                    .Include(p => p.Skills)
                    .Include(p => p.Stacks)
                    .Include(p => p.FreelanceTerms)
                    .Skip(i * batchSize)
                    .Take(batchSize)
                    .ToListAsync();

                profiles.ForEach(async profile =>
                {
                    var whatIsMissing = new List<string>();

                    profile.IsDeveloper = profile.Categories.Any(q => q.Category.Name.ToLower() == "developer");
                    profile.IsFreelancer = profile.Categories.Any(q => q.Category.Name.ToLower() == "freelancer");

                    if (string.IsNullOrEmpty(profile.Avatar))
                    {
                        whatIsMissing.Add("Your profile photo");
                    }

                    if (string.IsNullOrEmpty(profile.Bio))
                    {
                        whatIsMissing.Add("A brief description of you (bio)");
                    }

                    if (profile.Categories == null || profile.Categories?.Count() == 0)
                    {
                        whatIsMissing.Add("User category");
                    }

                    if (profile.Awards == null || profile.Awards?.Count() == 0)
                    {
                        whatIsMissing.Add("Your education history or awards");
                    }

                    if (profile.Skills == null || profile.Skills?.Count() == 0)
                    {
                        whatIsMissing.Add("Your skills");
                    }

                    if (profile.Projects == null || profile.Projects?.Count() == 0)
                    {
                        whatIsMissing.Add("Projects you have worked on");
                    }

                    if (profile.Contacts == null || profile.Contacts?.Count() == 0)
                    {
                        whatIsMissing.Add("Contact information");
                    }

                    if (profile.Employment == null || profile.Employment?.Count() == 0)
                    {
                        whatIsMissing.Add("Employment history");
                    }

                    // for freelancers
                    if (profile.IsFreelancer && profile.FreelanceTerms == null)
                    {
                        whatIsMissing.Add("Freelance rates");
                    }

                    // for developers
                    if (profile.IsDeveloper && (profile.Stacks == null || profile.Stacks?.Count() == 0))
                    {
                        whatIsMissing.Add("Developer Tech Stack");
                    }

                    if (whatIsMissing.Count > 0)
                    {
                        var sent = await ProcessEmail(profile, whatIsMissing);
                        if (sent)
                        {
                            _logger.LogInformation("Email sent");
                        }
                    }
                });
            }
        }

        public async Task<bool> ProcessEmail(Person person, List<string> checklist)
        {
            var emailDetails = new EmailDetailsDto
            {
                Subject = "Update your profile"
            };

            var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
            var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
            var emailTemplatePath = Path.Combine(_environment.WebRootPath, "EmailTemplates", "Reminders",
                "UpdateProfile.html");

            var emailBody = await File.ReadAllTextAsync(emailTemplatePath);

            var list = string.Empty;
            checklist.ForEach(c => list += $"<li style=\"padding: 5px 0;\">{c}</li>");

            emailBody = emailBody
                .Replace("[PERSON_NAME]", $"{person.Firstname}")
                .Replace("[PROFILE_URL]", $"{baseUrl}/profiles/people/{person.UserId}")
                .Replace("[CHECKLIST]", list);

            emailDetails.Body = emailBody;
            emailDetails.Recipient = person.Email;

            using var client = new HttpClient();

            var json = JsonConvert.SerializeObject(emailDetails, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                _logger.LogInformation("Sending email...");
                await client.PostAsync(emailEndpoint, content);
                _logger.LogInformation("Reminder sent");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return false;
            }
        }
    }
}