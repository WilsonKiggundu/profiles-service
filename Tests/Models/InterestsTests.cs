using System;
using System.Linq;
using ProfileService.Models;
using ProfileService.Models.Common;
using Tests.Helpers;
using Xunit;

namespace Tests.Models
{
    public class InterestsTests
    {
        [Fact]
        public void InterestsModel_CategoryShouldBeRequired()
        {
            var person = new Interest()
            {
                Category = string.Empty
            };

            var validationResult = ValidateModel.Validate(person);
            
            Assert.True(validationResult?.Any(v => 
                v.MemberNames.Contains("Category") && 
                v.ErrorMessage.Contains("required")));
        }
    }
}