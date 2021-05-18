using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProfileService.Contracts.FreelanceProject;
using ProfileService.Helpers;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class FreelanceProjectService : IFreelanceProjectService
    {
        private readonly IFreelanceProjectRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IDeviceService _deviceService;
        private readonly IWebNotification _notification;

        public FreelanceProjectService(IFreelanceProjectRepository repository, IWebHostEnvironment environment, IConfiguration configuration, IPersonRepository personRepository, IDeviceService deviceService, IWebNotification notification)
        {
            _repository = repository;
            _environment = environment;
            _configuration = configuration;
            _personRepository = personRepository;
            _deviceService = deviceService;
            _notification = notification;
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
            
            var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
            var person = await _personRepository.GetByIdAsync(hire.PersonId);
            var project = await _repository.GetByIdAsync(hire.ProjectId);
            
            // send app notification
            var devices 
                = await _deviceService.SearchAsync(hire.PersonId.ToString());

            if (devices.Any())
            {
                _notification.Send(devices, new NotificationPayload
                {
                    Title = $"{person.Firstname} {person.Lastname}" + " showed interest in your project",
                    Icon = person.Avatar,
                    Date = DateTime.UtcNow,
                    
                    Data = new
                    {
                        profileId = person.Id,
                        projectId = hire.ProjectId,
                        baseUrl = baseUrl
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
                        
                        // Body = comment.Details,
                        Tag = hire.PersonId.ToString(),
                        Icon = person.Avatar,
                    }
                });
            }

            // send email
            var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
            
            var emailTemplatePath = Path.Combine(_environment.ContentRootPath, "EmailTemplates", "FreelanceProjectInterest.html");
            var emailContent = await File.ReadAllTextAsync(emailTemplatePath);
            
            emailContent = emailContent
                .Replace("[PERSON_AVATAR]", person.Avatar)
                .Replace("[PROJECT NAME]", project.Name)
                .Replace("[PERSON_NAME]", $"{person.Firstname} {person.Lastname}")
                .Replace("[PROFILE_URL]", $"{baseUrl}/profiles/people/{hire.PersonId}");

            using var client = new HttpClient();
            var emailDetails = new
            {
                body = emailContent,
                recipient = project.OwnerEmail,
                senderEmail = "myvillage@devops.innovationvillage.co.ug",
                senderName = "MyVillage",
                subject = $"{person.Firstname} {person.Lastname} is interested in your project"
            };

            var emailJson = JsonSerializer.Serialize(emailDetails);
            var content = new StringContent(emailJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(emailEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateHireAsync(FreelanceProjectHire hire)
        {
            await _repository.UpdateHireAsync(hire);
        }
    }
}