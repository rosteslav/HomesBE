using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Properties.Application.Attributes
{
    public class ValidCsvFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.ContentType.EndsWith("/csv"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid file type. Only .csv files are accepted.");
        }
    }
}
