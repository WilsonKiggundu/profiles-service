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
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IWebNotification _notification;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FreelanceProjectService> _logger;

        public JobApplicationService(IJobApplicationRepository repository, IWebHostEnvironment environment,
            IConfiguration configuration, IPersonRepository personRepository, IWebNotification notification,
            ILogger<FreelanceProjectService> logger, IJobRepository jobRepository)
        {
            _repository = repository;
            _environment = environment;
            _configuration = configuration;
            _personRepository = personRepository;
            _notification = notification;
            _logger = logger;
            _jobRepository = jobRepository;
        }

        public async Task<ICollection<JobApplication>> SearchAsync(Guid jobId)    
        {
            return await _repository.SearchAsync(jobId);
        }

        public async Task<JobApplication> InsertAsync(JobApplication application)
        {
            application.Status = HireStatus.ExpressedInterest;
            application.Id = Guid.NewGuid();
            await _repository.InsertAsync(application);

            BackgroundJob.Enqueue(() => SendNotification(application));

            return application;
        }

        public async Task<JobApplication> UpdateAsync(JobApplication application)
        {
            await _repository.UpdateAsync(application);
            BackgroundJob.Enqueue(() => SendNotification(application));
            return application;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public async Task SendNotification(JobApplication application)
        {
            application.Job = await _jobRepository.GetJobAsync(application.JobId);
            application.Applicant = await _personRepository.GetByIdAsync(application.ApplicantId);

            string title;
            switch (application.Status)
            {
                case HireStatus.ExpressedInterest:
                    title = $"{application.Applicant.Firstname} has applied to the {application.Job.Title} role";
                    break;
                case HireStatus.Rejected:
                    title = $"Your application for the {application.Job.Title} role has been rejected";
                    break;
                case HireStatus.Considered:
                    title = $"You have been considered for the {application.Job.Title} role";
                    break;
                case HireStatus.Hired:
                    title = $"Congratulations! You have been hired for the {application.Job.Title} role.";
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
                    profileId = application.ApplicantId,
                    jobId = application.Job.Id
                },

                Options = new NotificationOptions
                {
                    Body = application.Remarks,
                    Tag = application.ApplicantId.ToString()
                }
            };

            // send app notification
            var devices = new List<Guid> {application.ApplicantId};
            BackgroundJob.Enqueue(() => _notification.SendAsync(devices, notification));

            // send email
            BackgroundJob.Enqueue(() => ProcessEmail(application, title));
        }

        public async Task ProcessEmail(JobApplication application, string subject)
        {
            var emailDetails = new EmailDetailsDto
            {
                Subject = subject
            };

            var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
            var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
            var emailTemplatePath = Path.Combine(_environment.WebRootPath, "EmailTemplates", "Jobs");

            string emailBody;

            switch (application.Status)
            {
                case HireStatus.ExpressedInterest:
                    emailTemplatePath = Path.Combine(emailTemplatePath, "Apply.html");
                    emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                    emailBody = emailBody
                        .Replace("[PERSON_AVATAR]", application.Applicant.Avatar)
                        .Replace("[JOB_TITLE]", application.Job.Title)
                        .Replace("[PERSON_NAME]", $"{application.Applicant.Firstname} {application.Applicant.Lastname}")
                        .Replace("[PROFILE_URL]",
                            $"{baseUrl}/profiles/people/{application.ApplicantId}?context=job_application&jobId={application.JobId}&requestId={application.Id}");

                    emailDetails.Body = emailBody;
                    emailDetails.Recipient = application.Job.ReplyEmail;

                    break;
                case HireStatus.Rejected:
                    emailTemplatePath = Path.Combine(emailTemplatePath, "Reject.html");
                    emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                    emailBody = emailBody
                        .Replace("[JOB_TITLE]", application.Job.Title)
                        .Replace("[PERSON_NAME]", $"{application.Applicant.Firstname} {application.Applicant.Lastname}")
                        .Replace("[MESSAGE]", application.Remarks);

                    emailDetails.Body = emailBody;
                    emailDetails.Recipient = application.Applicant.Email;

                    break;
                case HireStatus.Considered:
                    emailTemplatePath = Path.Combine(emailTemplatePath, "Consider.html");
                    emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                    emailBody = emailBody
                        .Replace("[JOB_TITLE]", application.Job.Title)
                        .Replace("[PERSON_NAME]", $"{application.Applicant.Firstname}")
                        .Replace("[EMAIL_ADDRESS]", $"{application.Job.ReplyEmail}")
                        .Replace("[MESSAGE]", application.Remarks);

                    emailDetails.Body = emailBody;
                    emailDetails.Recipient = application.Applicant.Email;
                    
                    break;
                case HireStatus.Hired:
                    emailTemplatePath = Path.Combine(emailTemplatePath, "Hire.html");
                    emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                    emailBody = emailBody
                        .Replace("[JOB_TITLE]", application.Job.Title)
                        .Replace("[PERSON_NAME]", $"{application.Applicant.Firstname}")
                        .Replace("[EMAIL_ADDRESS]", $"{application.Job.ReplyEmail}")
                        .Replace("[MESSAGE]", application.Remarks);

                    emailDetails.Body = emailBody;
                    emailDetails.Recipient = application.Applicant.Email;
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            using var client = new HttpClient();

            var json = JsonConvert.SerializeObject(emailDetails, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                _logger.LogInformation("Sending email...");
                await client.PostAsync(emailEndpoint, content);
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