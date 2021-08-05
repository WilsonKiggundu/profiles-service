using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.Zoom;
using ProfileService.Exceptions;
using ProfileService.Extensions;
using ProfileService.Helpers;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EventService : IEventService
    {
        private const string ZoomApiBaseUrl = "https://api.zoom.us/v2/";
        private const string ZoomId = "PmS3zY5gT1KyfKlVrcBcxQ";
        private const string ZoomApiKey = "P1TRDkOcTa6kp0G98KxB-g";
        private const string ZoomApiSecret = "tiG5x6C1eRtlsR9rapDgoE2SRw1PqVkFe3fX";

        private readonly string _zoomApiAccessToken;
        private readonly string _eventsApiBaseUrl;

        private readonly IPersonRepository _personRepository;
        private readonly ILogger<EventService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly IDeviceService _deviceService;
        private readonly IWebNotification _notification;

        public EventService(IConfiguration configuration, IPersonRepository personRepository,
            ILogger<EventService> logger, IWebHostEnvironment environment, IDeviceService deviceService,
            IWebNotification notification)
        {
            _configuration = configuration;
            _personRepository = personRepository;
            _logger = logger;
            _environment = environment;
            _deviceService = deviceService;
            _notification = notification;
            _eventsApiBaseUrl = configuration.GetSection("EventsService").Get<string>();
            _zoomApiAccessToken = ZoomAuth.GenerateJwtToken(ZoomApiKey, ZoomApiSecret);
        }

        public async Task<EventContract> CreateAsync(EventContract eventContract)
        {
            try
            {
                // if the event is a Zoom Webinar,
                // create the webinar automatically

                var createZoomWebinar
                    = eventContract.TivAffiliation
                      && eventContract.Type.Equals("webinar")
                      && eventContract.Location.Equals("On Zoom");

                if (createZoomWebinar)
                {
                    var webinar = await CreateWebinar(eventContract);
                    eventContract.WebinarId = webinar.Id.ToString();
                }

                using var client = new HttpClient();
                var json = JsonConvert.SerializeObject(eventContract);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var url = $"{_eventsApiBaseUrl}/api/events";

                var response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();
                var @event = JsonConvert.DeserializeObject<EventContract>(responseString);

                response.EnsureSuccessStatusCode();

                BackgroundJob.Enqueue(() => ProcessEmail(@event, false));
                BackgroundJob.Enqueue(() => SendNotification(@event));

                var startDateTime = Convert.ToDateTime(@event.StartDateTime);

                BackgroundJob.Schedule(() => 
                    ProcessEmail(@event, true), startDateTime.AddHours(-1));

                if (startDateTime.IsMoreThanDaysAway(2))
                {
                    BackgroundJob.Schedule(() => 
                        ProcessEmail(@event, true), startDateTime.AddDays(-1));
                }

                return @event;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task RegisterAsync(string eventId, Guid personId)
        {
            var @event = await GetByIdAsync(eventId);
            var person = await _personRepository.GetByIdAsync(personId);

            var isZoomWebinar
                = @event.TivAffiliation
                  && @event.Type.Equals("webinar")
                  && @event.Location.Equals("On Zoom");

            if (isZoomWebinar)
            {
                await RegisterForWebinar(@event.WebinarId, new WebinarRegistrant
                {
                    Email = person.Email,
                    FirstName = person.Firstname,
                    LastName = person.Lastname
                });
            }

            try
            {
                using var client = new HttpClient();
                var json = JsonConvert.SerializeObject(new
                {
                    category = "attending",
                    profileId = personId.ToString(),
                    id = Convert.ToInt64(eventId),
                    name = $"{person.Firstname} {person.Lastname}",
                    email = person.Email
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_eventsApiBaseUrl}/api/events/{eventId}/attendance";

                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<EventContract> UpdateAsync(EventContract eventContract)
        {
            try
            {
                using var client = new HttpClient();
                var json = JsonConvert.SerializeObject(eventContract);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_eventsApiBaseUrl}/api/events";

                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var @event = JsonConvert.DeserializeObject<EventContract>(responseString);

                return @event;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<ICollection<EventContract>> SearchAsync(EventSearch search)
        {
            try
            {
                var query = search.ToQueryString();

                using var client = new HttpClient();
                var url = $"{_eventsApiBaseUrl}/api/events?{query}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var events = JsonConvert.DeserializeObject<ICollection<EventContract>>(responseString);

                return events;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<ICollection<WebinarParticipant>> GetParticipantsAsync(string eventId)
        {
            var @event = await GetByIdAsync(eventId);

            var isZoomWebinar
                = @event.TivAffiliation
                  && @event.Type.Equals("webinar")
                  && @event.Location.Equals("On Zoom");

            if (isZoomWebinar)
                return await GetWebinarParticipants(eventId);

            return new List<WebinarParticipant>();
        }

        public async Task<ICollection<WebinarRegistrant>> GetRegistrantsAsync(string eventId)
        {
            var @event = await GetByIdAsync(eventId);
            if (@event is null) throw new NotFoundException();

            // var isZoomWebinar
            //     = @event.TivAffiliation
            //       && @event.Type.Equals("webinar")
            //       && @event.Location.Equals("On Zoom");
            //
            // if (isZoomWebinar) return await GetWebinarRegistrants(eventId);

            var registrants = new List<WebinarRegistrant>();

            @event.Attendances?.ForEach(person =>
            {
                if (string.IsNullOrEmpty(person.Name)) return;

                var nameArray = person.Name?.Split(" ");

                registrants.Add(new WebinarRegistrant
                {
                    Email = person.Email,
                    FirstName = nameArray[0],
                    LastName = nameArray[1],
                });
            });

            return registrants;
        }

        public async Task<EventContract> GetByIdAsync(string eventId)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{_eventsApiBaseUrl}/api/events/{eventId}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var @event = JsonConvert.DeserializeObject<EventContract>(responseString);

                if (string.IsNullOrEmpty(@event.WebinarId)) return @event;

                var webinar = await GetWebinarDetails(@event.WebinarId);
                @event.Webinar = webinar;

                return @event;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(string eventId)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{_eventsApiBaseUrl}/api/events/{eventId}";

                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        #region Zoom Webinars

        public async Task<WebinarResponse> CreateWebinar(EventContract @event)
        {
            var contactName = string.Empty;
            var contactEmail = string.Empty;

            if (@event.CreatedBy.HasValue)
            {
                var person = await _personRepository.GetByIdAsync(@event.CreatedBy.Value);
                contactEmail = person.Email;
                contactName = $"{person.Firstname} {person.Lastname}";
            }

            var startDateTime = Convert.ToDateTime(@event.StartDateTime);
            var endDateTime = Convert.ToDateTime(@event.EndDateTime);

            var webinar = new Webinar
            {
                Agenda = @event.Details,
                Duration = endDateTime.Subtract(startDateTime).TotalMinutes,
                Topic = @event.Title,
                Type = WebinarType.Webinar,
                StartTime = startDateTime,
                Settings = new WebinarSettings
                {
                    ApprovalType = WebinarApprovalType.Automatic,
                    RegistrationType = WebinarRegistrationType.RegisterOnceAttendAnySession,
                    // SurveyUrl = "",
                    RegistrantsEmailNotification = true,
                    MeetingAuthentication = true,
                    ContactEmail = contactEmail,
                    ContactName = contactName
                }
            };

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

            var json = JsonConvert.SerializeObject(webinar);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ZoomApiBaseUrl}/users/{ZoomId}/webinars";

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WebinarResponse>(responseString);
        }

        public async Task<WebinarRegistrationResponse> RegisterForWebinar(string webinarId,
            WebinarRegistrant registrant)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

            var json = JsonConvert.SerializeObject(registrant);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ZoomApiBaseUrl}/webinars/{webinarId}/registrants";

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WebinarRegistrationResponse>(responseString);
        }

        public async Task<ICollection<WebinarParticipant>> GetWebinarParticipants(string webinarId)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/past_webinars/{webinarId}/participants";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WebinarParticipantsResponse>(responseString);

                return data.Participants;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e);
                return new List<WebinarParticipant>();
            }
        }

        public async Task<ICollection<WebinarRegistrant>> GetWebinarRegistrants(string webinarId)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/webinars/{webinarId}/registrants";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WebinarRegistrantsResponse>(responseString);

                return data.Registrants;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<WebinarDetails> GetWebinarDetails(string webinarId)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/webinars/{webinarId}";

                var response = await client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WebinarDetails>(responseString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        #endregion


        #region Notifications

        public async Task SendNotification(EventContract @event)
        {
            var exclude = @event.CreatedBy?.ToString();

            var devices
                = await _deviceService.SearchAsync(exclude);

            _notification.Send(devices, new NotificationPayload
            {
                Title = "Upcoming Event",
                // Icon = person.Avatar,
                Date = DateTime.UtcNow,

                Data = new
                {
                    eventId = @event.Id,
                    baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>()
                },

                Options = new NotificationOptions
                {
                    Actions = new List<NotificationAction>
                    {
                        new NotificationAction
                        {
                            Action = "view-details",
                            Title = "Event details"
                        }
                    },

                    Body = @event.Title,
                    Tag = @event.Id.ToString(),
                }
            });
        }

        public void ProcessEmail(EventContract @event, bool isReminder = false)
        {
            var emails = _personRepository
                .GetAll()
                .Where(q => !string.IsNullOrEmpty(q.Email))
                .Select(s => new
                {
                    s.Firstname,
                    s.Lastname,
                    s.Email,
                    s.Id
                }).ToList();

            var contactEmail = emails.FirstOrDefault(q => q.Id.Equals(@event.CreatedBy))?.Email;

            if (emails.Any())
            {
                emails.ForEach(async person =>
                {
                    var baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>();
                    var emailEndpoint = _configuration.GetSection("EmailEndpoint").Get<string>();

                    var templateFile = isReminder ? "EventReminder.html" : "EventPosted.html";

                    var emailTemplatePath =
                        Path.Combine(_environment.WebRootPath, "EmailTemplates", "Events", templateFile);

                    try
                    {
                        var body = await File.ReadAllTextAsync(emailTemplatePath);
                        var startDateTime = Convert.ToDateTime(@event.StartDateTime);
                        var endDateTime = Convert.ToDateTime(@event.EndDateTime);
                        var duration = endDateTime.Subtract(startDateTime).TotalMinutes;

                        body = body
                            .Replace("[PERSON_NAME]", $"{person.Firstname}")
                            .Replace("[EVENT_TITLE]", $"{@event.Title}")
                            .Replace("[EVENT_DATE]", $"{startDateTime:f}")
                            .Replace("[EVENT_DURATION]", $"{duration} minutes")
                            .Replace("[EVENT_LOCATION]", $"{@event.Location}")
                            .Replace("[EVENT_CONTACT_EMAIL]", $"{contactEmail}")
                            .Replace("[EVENT_URL]", $"{baseUrl}/events/{@event.Id}");

                        var emailDetails = new EmailDetailsDto
                        {
                            Body = body,
                            Subject = $"[EVENT] {@event.Title}",
                            Recipient = person.Email
                        };

                        using var client = new HttpClient();

                        var json = JsonConvert.SerializeObject(emailDetails, Formatting.Indented);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        await client.PostAsync(emailEndpoint, content);
                        _logger.LogInformation("Email sent");
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical("Error while sending email");
                        throw new Exception(e.Message, e);
                    }
                });
            }
        }

        #endregion
    }
}