using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace ProfileService.Extensions
{
    public class NoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull]DashboardContext context)
        {
            return true;
        }
    }
}