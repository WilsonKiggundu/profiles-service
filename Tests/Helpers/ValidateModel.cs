using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests.Helpers
{
    public static class ValidateModel
    {

        public static bool IsValidGuid(string guidStr)
        {
            return Guid.TryParse(guidStr, out var result);
        }
        
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, results, true);
            return results;
        }
    }
}