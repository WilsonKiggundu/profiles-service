using System;
using System.Collections.Generic;
using ProfileService.Models.Common;

namespace ProfileService.Models
{
    public class FreelanceProject : BaseModel
    {
        public Guid? OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Upload> Uploads { get; set; }
        public string Skills { get; set; }
        public string PaymentOption { get; set; }
        public string Budget { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Open;
        public Guid? HiredPersonId { get; set; }
        public Person.Person HiredPerson { get; set; }
    }

    public enum ProjectStatus
    {
        Open = 1,
        Taken = 2,
        OnHold = 3
    }
}