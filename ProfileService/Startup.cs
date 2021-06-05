using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProfileService.Exceptions;
using ProfileService.Extensions;
using ProfileService.Models.Common;
using ProfileService.Repositories;
using ProfileService.Repositories.Implementations;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Implementations;
using ProfileService.Services.Interfaces;

namespace ProfileService
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public readonly string AllowedOrigins = "_allowedOrigins";
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddHttpClient();
            services.AddResponseCaching();
            
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddAutoMapper(typeof(Startup));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProfileServiceContext>(options => options.UseNpgsql(connectionString));

            services.AddHangfire(x => x.UsePostgreSqlStorage(connectionString));
            services.AddDependencyInjection();
            services.AddEmailSenders(Configuration);

            services.AddSwaggerGen(context =>
            {
                context.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0",
                    Title = "Profiles Service",
                    
                    Description = "A service for managing profiles",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilson Kiggundu",
                        Email = "wkiggundu@innovationvillage.co.ug",
                        Url = new Uri("https://innovationvillage.co.ug")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                context.IncludeXmlComments(xmlPath);
            });
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration.Authority();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuer = Configuration.Authority()
                    };
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundService backgroundService)
        {
            
            
            app.UseExceptionHandler(error => error.UseCustomErrors(env));

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(context =>
            {
                context.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
                context.RoutePrefix = "docs";
            });

            app.UseCors();

            app.UseReDoc(options =>
            {
                options.SpecUrl("/swagger/v1/swagger.json");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new List<IDashboardAuthorizationFilter>
                {
                    new NoAuthFilter()
                }
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ProfileServiceContext>();
                
                context.Database.Migrate();
                
                var categories = new List<string>
                {
                    "Investor", 
                    "Student", 
                    "Data Scientist",
                    "Developer",
                    "Freelancer",
                    "Entrepreneur", 
                    "Professional", 
                    "Intern" 
                };
                
                categories.ForEach(category =>
                {
                    var exists = context.LookupCategories.Any(c => c.Name == category);
                    if (!exists)
                        context.LookupCategories.Add(new Category
                        {
                            Name = category
                        });
                });

                context.SaveChanges();
                RecurringJob.AddOrUpdate(() => backgroundService.SendProfileUpdateRemindersAsync(1, 100), Cron.Monthly);
                
            }
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}