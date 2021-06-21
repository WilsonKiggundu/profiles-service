using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using ProfileService.Models.Common;

namespace ProfileService.Models.Employees
{
    [Table("Employees", Schema = "employees")]
    public class Employee : BaseModel
    {
        public Guid BusinessId { get; set; }
        public Department? Department { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string Position { get; set; }

        public Unit? Unit { get; set; }
    }

    public enum Unit
    {
        Marketplace = 1,
        Textile = 2,
        MachineShop = 3,
        Wood = 4,
        Tribe = 5,
        TheKitchen = 6,
        Academy = 7,
        BusinessOperations = 8,
        Finance = 9,
        SalesAndMarketing = 10,
        Admin = 11,
        CorporateServices = 12,
        Programs = 13,
        SpecialProjects = 14,
        Technology = 14,
        Operations = 16,
        Tukole = 17,
        Upskill = 18,
        Community = 19,
        FutureLab = 20,
        Other = 99
    }

    public enum Department
    {
        CorporateServices = 1,
        Programs = 2,
        SpecialProjects = 3,
        Technology = 4,
        Finance = 5,
        Operations = 6,
        Motiv = 7,
        TeamLead = 8,
        Other = 99
    }
}