using System;
using Microsoft.Extensions.Configuration;

namespace ProfileService.Extensions
{
    public static class ConfigExtensions
    {    
        public static string Authority(this IConfiguration config)
        {
            var authority = config.GetValue<string>("Authentication:Authority");

            Console.WriteLine(authority);
            
            return authority;
        }
    }
}