using Microsoft.Extensions.DependencyInjection;
using ProfileService.Repositories.Implementations;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Extensions
{        
    public static class DependencyInjection
    {
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