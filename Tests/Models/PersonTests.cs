using System;
using System.Linq;
using ProfileService.Models;
using Tests.Helpers;
using Xunit;

namespace Tests.Models
{
    public class PersonTests
    {
        [Fact]
        public void PersonModel_FirstnameShouldBeRequired()
        {
            var person = new Person
            {
                UserId = Guid.NewGuid(),
                Lastname = "Last name"
            };

            var validationResult = ValidateModel.Validate(person);
            
            Assert.True(validationResult?.Any(v => 
                v.MemberNames.Contains("Firstname") && 
                v.ErrorMessage.Contains("required")));
        }
        
        [Fact]
        public void PersonModel_LastnameShouldBeRequired()
        {
            var person = new Person
            {
                UserId = Guid.NewGuid(),
                Firstname = "First name"
            };

            var validationResult = ValidateModel.Validate(person);
            
            Assert.True(validationResult?.Any(v => 
                v.MemberNames.Contains("Lastname") && 
                v.ErrorMessage.Contains("required")));
        }
    }
}