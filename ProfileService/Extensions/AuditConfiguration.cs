using Audit.Core;
using Audit.PostgreSql.Configuration;
using Audit.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileService.Extensions
{
    public static class AuditConfiguration
    {
        public static void AddAudit(MvcOptions options)
        {
            options.AddAuditFilter(config => config
                .LogAllActions()
                .WithEventType("{verb} {controller}.{action}")
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseHeaders()
            );
        }
        
        // Configures what and how is logged or is not logged
        public static void ConfigureAudit(string connectionString)
        {
            Configuration.Setup()
                .UsePostgreSql(config => config
                    .ConnectionString(connectionString)
                    .TableName("AuditLogs")
                    .IdColumnName("Id")
                    .Schema("audit")
                    .DataColumn("Data", DataType.JSONB)
                    .LastUpdatedColumnName("UpdatedDate")
                    .CustomColumn("InsertedDate", ev => ev.StartDate)
                    .CustomColumn("EventType", ev => ev.EventType)
                    .CustomColumn("User", ev => ev.Environment.UserName)
                );
        }
    }
}