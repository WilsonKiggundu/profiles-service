using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using ProfileService.Models.Common;
using ProfileService.Models.Employees;
using ProfileService.Models.Preferences;
using ProfileService.Repositories;
using WebPush;

namespace ProfileService.Helpers
{
    public static class DataSeeder
    {

        public static void Employees(ProfileServiceContext context, IWebHostEnvironment environment)
        {
            if (context.Employees.Any()) return;

            var businessId = Guid.NewGuid();
            var employees = new List<Employee>();
            var path = Path.Combine(environment.WebRootPath, "Data", "Employees.csv");

            var lines = File.ReadAllLines(path).Skip(1).Select(a => a.Split(';')).ToList();
            lines.ForEach(line =>
            {
                var employee = line[0].Split(',');
                var name = employee[0].Split(' ');
                var nameLength = name.Length;
                var firstname = name[0];
                var lastname = string.Join(' ', name.Skip(1).Take(nameLength - 1).ToList());
                var position = employee[1];
                var department = employee[2];
                var unit = employee[3];
                
                employees.Add(new Employee
                {
                    BusinessId = businessId,
                    Firstname = firstname,
                    Lastname = lastname,
                    Position = position,
                    Unit = unit switch
                    {
                        "Marketplace" => Unit.Marketplace,
                        "Textile" => Unit.Textile,
                        "Machine Shop" => Unit.MachineShop,
                        "Wood" => Unit.Wood,
                        "Tribe" => Unit.Tribe,
                        "The Kitchen" => Unit.TheKitchen,
                        "Academy" => Unit.Academy,
                        "Business and Operations" => Unit.BusinessOperations,
                        "Finance" => Unit.Finance,
                        "Sales and Marketing" => Unit.SalesAndMarketing,
                        "Admin Officers" => Unit.Admin,
                        "Corporate Services" => Unit.CorporateServices,
                        "Operations" => Unit.Operations,
                        "Programs" => Unit.Programs,
                        "Projects" => Unit.SpecialProjects,
                        "Technology and Data Services" => Unit.Technology,
                        _ => Unit.Other
                    },
                    Department = department switch {
                        "Corporate Services" => Department.CorporateServices,
                        "Finance" => Department.Finance,
                        "Operations" => Department.Operations,
                        "Programs" => Department.Programs,
                        "Projects" => Department.SpecialProjects,
                        "Technology and Data Services" => Department.Technology,
                        "Motiv" => Department.Motiv,
                        _ => Department.Other,
                    },
                });
            });

            if (!employees.Any()) return;

            try
            {
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        
        public static void SeedEmailPreferences(ProfileServiceContext context)
        {
            if (context.EmailPreferences.Any()) return;
            
            var settings = context.Persons.Select(s => new EmailSettings{PersonId = s.Id}).ToList();
            context.EmailPreferences.AddRange(settings);
            context.SaveChanges();
        }

        public static void GenerateVapidKeys(ProfileServiceContext context)
        {
            if (context.VapidKeys.Any()) return;
            var keys = VapidHelper.GenerateVapidKeys();

            context.VapidKeys.Add(new VapidKeys
            {
                PublicKey = keys.PublicKey,
                PrivateKey = keys.PrivateKey,
                Subject = keys.Subject
            });

            context.SaveChanges();
        }
    }
}