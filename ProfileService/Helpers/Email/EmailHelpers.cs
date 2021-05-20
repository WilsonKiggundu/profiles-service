using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProfileService.Contracts;

namespace ProfileService.Helpers.Email
{
    public static class EmailHelpers
    {
        public static async Task SendAsync(string endpoint, EmailDetailsDto body)
        {
            using var client = new HttpClient();

            var emailJson = JsonSerializer.Serialize(body);
            var content = new StringContent(emailJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
        }
    }
}