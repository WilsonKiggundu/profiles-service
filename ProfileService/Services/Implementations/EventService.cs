using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts;
using ProfileService.Contracts.Zoom;
using ProfileService.Extensions;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EventService : IEventService
    {
        private const string ZoomApiBaseUrl = "https://api.zoom.us/v2/";
        private const string ZoomId = "PmS3zY5gT1KyfKlVrcBcxQ";
        private readonly string _eventsApiBaseUrl;

        public EventService(IConfiguration configuration)
        {
            _eventsApiBaseUrl = configuration.GetSection("EventsService").Get<string>();
        }

        public async Task<EventContract> CreateAsync(EventContract eventContract)
        {
            try
            {
                // if the event is a Zoom Webinar,
                // create the webinar automatically
                if (eventContract.IsZoomEvent)
                {
                    var webinar = await CreateWebinar(eventContract);
                    eventContract.ConferenceUrl = webinar.Uuid;
                }
                
                using var client = new HttpClient();
                var json = JsonConvert.SerializeObject(eventContract);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{_eventsApiBaseUrl}/api/events";

                var response = await client.PostAsync(url, content);
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

        public async Task<ICollection<WebinarParticipant>> GetParticipantsAsync(string eventId, string type)
        {
            if (type == "webinar") return await GetWebinarParticipants(eventId);
            return new List<WebinarParticipant>();
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
                return JsonConvert.DeserializeObject<EventContract>(responseString);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(string eventId)
        {
            throw new NotImplementedException();
        }

        #region Zoom Webinars

        public async Task<WebinarResponse> CreateWebinar(EventContract @event)
        {
            var webinar = new Webinar
            {
                Agenda = @event.Details,
                Duration = @event.EndDateTime.Subtract(@event.StartDateTime).TotalMinutes,
                Topic = @event.Title,
                Type = WebinarType.Webinar,
                StartTime = @event.StartDateTime,
            };
            
            using var client = new HttpClient();
            var json = JsonConvert.SerializeObject(webinar);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ZoomApiBaseUrl}/users/{ZoomId}/webinars";

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
                
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WebinarResponse>(responseString);
        }

        public async Task<WebinarRegistrationResponse> RegisterForWebinar(string webinarId, WebinarRegistrant registrant)
        {           
            using var client = new HttpClient();
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
                var url = $"{ZoomApiBaseUrl}/past_webinars/{webinarId}/participants";
                
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WebinarParticipantsResponse>(responseString);
                
                return data.Participants;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        #endregion
        
    }
}