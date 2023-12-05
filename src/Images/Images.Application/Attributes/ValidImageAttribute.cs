using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Images.Application.Attributes
{
    public class ValidImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.ContentType.StartsWith("image/"))
            {
                var imgInMB = file.Length / 1024 / 1024;

                if (imgInMB > 5)
                {
                    return new ValidationResult("Invalid file size. Only images up to 5 MB are accepted.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid file type. Only image files are accepted.");
        }
    }
}
