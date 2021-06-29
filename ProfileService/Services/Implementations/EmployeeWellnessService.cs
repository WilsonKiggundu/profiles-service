using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.Employee;
using ProfileService.Extensions;
using ProfileService.Models;
using ProfileService.Models.Employees;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EmployeeWellnessService : IEmployeeWellnessService
    {
        private readonly IEmployeeWellnessRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EmployeeWellnessService> _logger;

        public EmployeeWellnessService(IEmployeeWellnessRepository repository, IEmployeeRepository employeeRepository, IConfiguration configuration, IWebHostEnvironment environment, ILogger<EmployeeWellnessService> logger)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeWellness>> SearchAsync(SearchEmployeeRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<EmployeeWellness> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<EmployeeWellness> InsertAsync(EmployeeWellness wellness)
        {
            await _repository.AddOrUpdateAsync(wellness);
            await SendNotification(wellness);
            return wellness;
        }

        public async Task<EmployeeWellness> UpdateAsync(EmployeeWellness wellness)
        {
            await _repository.UpdateAsync(wellness);
            await SendNotification(wellness);
            return wellness;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
        
        public async Task SendNotification(EmployeeWellness wellness)
        {
            var employee = await _employeeRepository.GetByIdAsync(wellness.EmployeeId);
            wellness.Employee = employee;
            
            string title;
            switch (wellness.Status)
            {
                case Wellness.Unwell:
                    title = $"{employee.Firstname} is not well";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // send email
            BackgroundJob.Enqueue(() => ProcessEmail(wellness, title));
        }

        public async Task ProcessEmail(EmployeeWellness wellness, string subject)
        {
            var emailDetails = new EmailDetailsDto
            {
                Subject = subject
            };

            var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();
            var emailTemplatePath = Path.Combine(_environment.WebRootPath, "EmailTemplates", "TheSpartans");
            var recipients = new List<string>()
            {
                "wkiggundu@innovationvillage.co.ug"
            };

            switch (wellness.Status)
            {
                case Wellness.Unwell:
                    emailTemplatePath = Path.Combine(emailTemplatePath, "WellnessUpdate.html");
                    var emailBody = await File.ReadAllTextAsync(emailTemplatePath);

                    emailBody = emailBody
                        .Replace("[STATUS]", wellness.Status.GetDisplayName())
                        .Replace("[MESSAGE]", wellness.Details)
                        .Replace("[PERSON_NAME]", $"{wellness.Employee.Firstname} {wellness.Employee.Lastname}");

                    emailDetails.Body = emailBody;
                    emailDetails.Recipient = string.Join(",", recipients);

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