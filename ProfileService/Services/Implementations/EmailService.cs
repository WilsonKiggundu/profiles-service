using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Common;
using ProfileService.Helpers.Email;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProfileService.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SendGridConfiguration _configuration;
        private readonly ISendGridClient _client;
        private readonly IWebHostEnvironment _environment;
        private readonly IPersonRepository _personRepository;

        public EmailService(ILogger<EmailService> logger, SendGridConfiguration configuration, ISendGridClient client, IWebHostEnvironment environment, IPersonRepository personRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _client = client;
            _environment = environment;
            _personRepository = personRepository;
        }

        private async Task<string> ProcessMessageBody(EmailDetails details)
        {
            string content;

            if (details.Template == EmailTemplate.IfYourProfileIsIncomplete)
            {
                var path = Path.Combine(_environment.ContentRootPath, "EmailTemplates", "CompleteProfile.html");
                content = await File.ReadAllTextAsync(path);

                var checkList = "";
                
                var preferences = await _personRepository.EmailPreferences(details.PersonId);
                
                
            }
            else
            {
                content = string.Empty;
            }
            
            return content;
        }
        
        public async Task SendEmailAsync(EmailDetails details)
        {
            var htmlMessage = await ProcessMessageBody(details);
            
            var message = MailHelper.CreateSingleEmail(
                new EmailAddress(_configuration.SourceEmail, _configuration.SourceName),
                new EmailAddress(details.Recipient),
                details.Subject,
                null,
                htmlMessage
            );
            
            message.SetClickTracking(_configuration.EnableClickTracking, _configuration.EnableClickTracking);
            var response = await _client.SendEmailAsync(message);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    _logger.LogInformation($"{JsonConvert.SerializeObject(details)} successfully sent");
                    break;
                default:
                {
                    var errorMessage = await response.Body.ReadAsStringAsync();
                    _logger.LogError($"Response with code {response.StatusCode} and body {errorMessage} " +
                                     $"after sending email: {JsonConvert.SerializeObject(details)}");
                    break;
                }
            }
        }
    }
}