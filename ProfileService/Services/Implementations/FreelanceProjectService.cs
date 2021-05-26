using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Helpers;
using ProfileService.Helpers.Email;
using ProfileService.Models;
using ProfileService.Models.Common;
using ProfileService.Models.Person;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class FreelanceProjectService : IFreelanceProjectService
    {
        private readonly IFreelanceProjectRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IWebNotification _notification;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FreelanceProjectService> _logger;

        public FreelanceProjectService(IFreelanceProjectRepository repository, IWebHostEnvironment environment,
            IConfiguration configuration, IPersonRepository personRepository, IWebNotification notification,
            ILogger<FreelanceProjectService> logger)
        {
            _repository = repository;
            _environment = environment;
            _configuration = configuration;
            _personRepository = personRepository;
            _notification = notification;
            _logger = logger;
        }

        public async Task<SearchFreelanceProjectResponse> SearchAsync(SearchFreelanceProjectRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<FreelanceProject> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<FreelanceProject> InsertAsync(FreelanceProject project)
        {
            project.Id = Guid.NewGuid();
            await _repository.InsertAsync(project);

            return project;
        }

        public async Task<FreelanceProject> UpdateAsync(FreelanceProject project)
        {
            await _repository.UpdateAsync(project);
            return project;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<FreelanceProjectHire>> GetHiresAsync(Guid projectId)
        {
            return await _repository.GetHiresAsync(projectId);
        }

        public async Task AddHireAsync(FreelanceProjectHire hire)
        {
            await _repository.AddHireAsync(hire);
            var person = await _personRepository.GetByIdAsync(hire.PersonId);
            var project = await _repository.GetByIdAsync(hire.ProjectId);

            #region send app notification

            if (project.OwnerId.HasValue)
            {
                var devices = new List<Guid> {project.OwnerId.Value};
                var notification = new NotificationPayload
                {
                    Title = $"{person.Firstname} {person.Lastname}" + " showed interest in your project",
                    Icon = person.Avatar,
                    Date = DateTime.UtcNow,

                    Data = new
                    {
                        profileId = person.Id,
                        projectId = hire.ProjectId
                    },

                    Options = new NotificationOptions
                    {
                        Actions = new List<NotificationAction>
                        {
                            new NotificationAction
                            {
                                Action = "view-profile",
                                Title = "View profile"
                            }
                        },

                        Body = project.Name,
                        Tag = hire.PersonId.ToString(),
                        Icon = person.Avatar,
                    }
                };
                BackgroundJob.Enqueue(() => _notification.SendAsync(devices, notification));
            }

            #endregion

            // send email
            BackgroundJob.Enqueue(() => ProcessEmail(hire));
        }

        public async Task UpdateHireAsync(FreelanceProjectHire hire)
        {
            await _repository.UpdateHireAsync(hire);

            var title = string.Empty;
            switch (hire.Status)
            {
                case HireStatus.ExpressedInterest:
                    break;
                case HireStatus.Rejected:
                    title = "Your request has been rejected";
                    break;
                case HireStatus.Considered:
                    title = "You have been considered for a project";
                    break;
                case HireStatus.Hired:
                    title = "Congratulations! You have been hired.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var notification = new NotificationPayload
            {
                Title = title,
                Date = DateTime.UtcNow,

                Data = new
                {
                    profileId = hire.PersonId,
                    projectId = hire.ProjectId
                },

                Options = new NotificationOptions
                {
                    Body = hire.Message,
                    Tag = hire.PersonId.ToString()
                }
            };

            // send app notification
            var devices = new List<Guid> {hire.PersonId};
            BackgroundJob.Enqueue(() => _notification.SendAsync(devices, notification));

            // send email
            BackgroundJob.Enqueue(() => ProcessEmail(hire));
        }

        public async Task ProcessEmail(FreelanceProjectHire hire)
        {
            var person = await _personRepository.GetByIdAsync(hire.PersonId);

            if (!string.IsNullOrEmpty(person.Email))
            {
                var emailDetails = new EmailDetailsDto
                {
                    Body = string.Empty,
                    Recipient = person.Email,
                    Subject = string.Empty
                };

                var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
                var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
                var emailTemplatePath = Path.Combine(_environment.WebRootPath, "EmailTemplates");
                var project = await _repository.GetByIdAsync(hire.ProjectId);

                string emailBody;

                switch (hire.Status)
                {
                    case HireStatus.ExpressedInterest:
                        emailTemplatePath = Path.Combine(emailTemplatePath, "FreelanceProjectInterest.html");
                        emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                        emailBody = emailBody
                            .Replace("[PERSON_AVATAR]", person.Avatar)
                            .Replace("[PROJECT NAME]", project.Name)
                            .Replace("[PERSON_NAME]", $"{person.Firstname} {person.Lastname}")
                            .Replace("[PROFILE_URL]",
                                $"{baseUrl}/profiles/people/{hire.PersonId}?context=freelance_project&projectId={project.Id}&requestId={hire.Id}");

                        emailDetails.Body = emailBody;
                        emailDetails.Subject = $"{person.Firstname} {person.Lastname} is interested in your project";

                        break;
                    case HireStatus.Rejected:
                        emailTemplatePath = Path.Combine(emailTemplatePath, "RejectFreelanceProjectInterest.html");
                        emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                        emailBody = emailBody
                            .Replace("[PROJECT NAME]", project.Name)
                            .Replace("[PERSON_NAME]", $"{person.Firstname} {person.Lastname}")
                            .Replace("[MESSAGE]", hire.Message);

                        emailDetails.Body = emailBody;
                        emailDetails.Subject = "Your request has been rejected";

                        break;
                    case HireStatus.Considered:
                        emailTemplatePath = Path.Combine(emailTemplatePath, "RequestMoreInfoFreelanceProjectInterest.html");
                        emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                        emailBody = emailBody
                            .Replace("[PROJECT_NAME]", project.Name)
                            .Replace("[PERSON_NAME]", $"{person.Firstname}")
                            .Replace("[EMAIL_ADDRESS]", $"{project.OwnerEmail}")
                            .Replace("[MESSAGE]", hire.Message);

                        emailDetails.Body = emailBody;
                        emailDetails.Subject = "Your request has been considered";
                        break;
                    case HireStatus.Hired:
                        emailTemplatePath = Path.Combine(emailTemplatePath, "HireFreelanceProjectInterest.html");
                        emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                        emailBody = emailBody
                            .Replace("[PROJECT_NAME]", project.Name)
                            .Replace("[PERSON_NAME]", $"{person.Firstname}")
                            .Replace("[MESSAGE]", hire.Message);

                        emailDetails.Body = emailBody;
                        emailDetails.Subject = "Congratulations! You have been hired.";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(emailDetails, Formatting.Indented);
                _logger.LogInformation(json);
                
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    _logger.LogInformation("Sending email...");
                    var response = await client.PostAsync(emailEndpoint, content);
                    _logger.LogInformation(response.StatusCode.ToString());
                    
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.Message);
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}