using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Helpers.Email;
using ProfileService.Repositories.Implementations;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using SendGrid;

namespace ProfileService.Extensions
{        
    public static class DependencyInjection
    {
        public static void AddEmailSenders(this IServiceCollection services, IConfiguration configuration)
        {
            var sendGridConfiguration = configuration.GetSection(nameof(SendGridConfiguration)).Get<SendGridConfiguration>();

            if (sendGridConfiguration != null && !string.IsNullOrWhiteSpace(sendGridConfiguration.ApiKey))
            {
                services.AddSingleton<ISendGridClient>(_ => new SendGridClient(sendGridConfiguration.ApiKey));
                services.AddSingleton(sendGridConfiguration);
            }
        }
        
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                
                .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            
                .AddClasses(classes => classes.AssignableTo<IService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}