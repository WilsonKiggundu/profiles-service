using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProfileService.Helpers;
using ProfileService.Helpers.Email;
using ProfileService.Repositories;
using Serilog;
using Serilog.Events;

namespace ProfileService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/profile-service.txt", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            
            try
            {
                
                Log.Information("Starting up");
                
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var environment = scope.ServiceProvider.GetService<IWebHostEnvironment>();
                    var context = scope.ServiceProvider.GetService<ProfileServiceContext>();
                    context.Database.Migrate();
                    
                    // DataSeeder.Employees(context, environment);
                    // DataSeeder.SeedEmailPreferences(context);
                    // DataSeeder.GenerateVapidKeys(context);
                }
                
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}