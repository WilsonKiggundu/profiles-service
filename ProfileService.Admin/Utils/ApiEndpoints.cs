namespace ProfileService.Admin.Utils
{
    public static class Services
    {    
        public static string ProfileServiceBaseUrl = "https://profiles-test.innovationvillage.co.ug";
        public static string JobsServiceBaseUrl = "https://jobs-test.innovationvillage.co.ug";
        public static string EventsServiceBaseUrl = "https://events-test.innovationvillage.co.ug";
    }
    
    public static class Endpoints
    {
        public static string Person { get; set; } = $"{Services.ProfileServiceBaseUrl}/api/person";
    }
}