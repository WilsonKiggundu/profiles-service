using System;
using ProfileService.Models;

namespace ProfileService.Contracts.Common
{
    public class EmailDetails
    {
        public Guid PersonId { get; set; }
        public string Subject { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public EmailType Template { get; set; }
    }
}