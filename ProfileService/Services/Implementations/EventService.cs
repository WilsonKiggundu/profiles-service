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
using ProfileService.Contracts.Blog.Post;
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
        private readonly IPostService _postService;

        public EventService(IConfiguration configuration, IPersonRepository personRepository,
            ILogger<EventService> logger, IWebHostEnvironment environment, IDeviceService deviceService,
            IWebNotification notification, IPostService postService)
        {
            _configuration = configuration;
            _personRepository = personRepository;
            _logger = logger;
            _environment = environment;
            _deviceService = deviceService;
            _notification = notification;
            _postService = postService;
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

                if (@event.CreatedBy != null)
                {
                    var newPost = new NewPost
                    {
                        Type = PostType.Event,
                        AuthorId = @event.CreatedBy.Value,
                        Title = @event.Title,
                        Details = @event.Details,
                        Ref = @event.Id
                    };
            
                    // add post
                    BackgroundJob.Enqueue(() => _postService.InsertAsync(newPost));
                }

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

            var isZoomEvent
                = @event.TivAffiliation
                  && (@event.Type.Equals("webinar") || @event.Type.Equals("meeting"))
                  && @event.Location.Equals("On Zoom");

            if (isZoomEvent)
            {
                var eventType = @event.Type.Equals("Webinar") ? EventType.Webinar : EventType.Meeting;
                await RegisterForZoomEvent(eventType, @event.WebinarId, new Registrant
                {
                    Email = person.Email,
                    FirstName = person.Firstname,
                    LastName = person.Lastname,
                    CustomQuestions = new List<CustomQuestion>
                    {
                        // new CustomQuestion
                        // {
                        //     Title = "age",
                        //     Value = "12"
                        // }
                    }
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

        public async Task<ICollection<Participant>> GetParticipantsAsync(EventType type, string id)
        {
            var @event = await GetByIdAsync(id);

            var isZoomEvent
                = @event.TivAffiliation
                  && (@event.Type.Equals("webinar") || @event.Type.Equals("meeting"))
                  && @event.Location.Equals("On Zoom");

            if (isZoomEvent)
                return await GetParticipants(type, @event.WebinarId);

            return new List<Participant>();
        }

        public async Task<ICollection<Registrant>> GetRegistrantsAsync(EventType type, string eventId)  
        {   
            var @event = await GetByIdAsync(eventId);
            if (@event is null) throw new NotFoundException();

            var isZoomEvent
                = @event.TivAffiliation
                  && (@event.Type.Equals("webinar") || @event.Type.Equals("meeting"))
                  && @event.Location.Equals("On Zoom");
            
            if (isZoomEvent) return await GetRegistrants(type, eventId);
            var registrants = new List<Registrant>();

            @event.Attendances?.ForEach(person =>
            {
                if (string.IsNullOrEmpty(person.Name)) return;

                var nameArray = person.Name?.Split(" ");

                registrants.Add(new Registrant
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

                var eventType = @event.Type.Equals("webinar") ? EventType.Webinar : EventType.Meeting;
                var webinar = await GetEventDetails(eventType, @event.WebinarId);
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

        #region Zoom Webinars and Meetings
        public async Task<MeetingResponse> CreateMeeting(EventContract @event)
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

            var meeting = new Meeting
            {
                Agenda = @event.Details,
                Duration = endDateTime.Subtract(startDateTime).TotalMinutes,
                Topic = @event.Title,
                Type = MeetingType.Scheduled,
                StartTime = startDateTime,
                Settings = new MeetingSettings
                {
                    ContactEmail = contactEmail,
                    ContactName = contactName
                }
            };

            if (@event.IsRecurring)
            {
                meeting.Recurrence = new WebinarRecurrence
                {
                    // Type = @event.Frequency,
                    // RepeatInterval = ,
                    // WeeklyDays = ,
                };
            }

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

            var json = JsonConvert.SerializeObject(meeting);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ZoomApiBaseUrl}/users/{ZoomId}/meetings";

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MeetingResponse>(responseString);
        }
        
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
                    RegistrationType = WebinarRegistrationType.RegisterOnceChooseOneOrMoreSessions,
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
        public async Task RegisterForZoomEvent(EventType type, string eventId, Registrant registrant)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

            var json = JsonConvert.SerializeObject(registrant);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ZoomApiBaseUrl}/{type.ToString().ToLower()}s/{eventId}/registrants";

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
        }

        private async Task<ICollection<Participant>> GetParticipants(EventType type, string id)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/past_{type.ToString().ToLower()}s/{id}/participants";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ParticipantsResponse>(responseString);

                return data.Participants;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e);
                return new List<Participant>();
            }
        }

        private async Task<ICollection<Registrant>> GetRegistrants(EventType type, string eventId)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/{type.ToString().ToLower()}s/{eventId}/registrants";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<RegistrantsResponse>(responseString);

                return data.Registrants;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<WebinarDetails> GetEventDetails(EventType type, string id)
        {
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _zoomApiAccessToken);

                var url = $"{ZoomApiBaseUrl}/{type.ToString().ToLower()}s/{id}";

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
                Title = $"Upcoming Event: {@event.Title} on {Convert.ToDateTime(@event.StartDateTime):f}" ,
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
                            .Replace("[EVENT_DETAILS]", $"{@event.Details}")
                            .Replace("[EVENT_CONTACT_EMAIL]", $"{contactEmail}")
                            .Replace("[EVENT_URL]", $"{baseUrl}/events/{@event.Id}/details");

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