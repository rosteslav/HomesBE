using Demo.Application.Models.Security;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Attributes.Security
{
    public class ValidRoleAttribute : ValidationAttribute
    {
        private readonly string[] validRoles =
        {
            UserRoles.Buyer,
            UserRoles.Seller,
            UserRoles.Broker
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string role && validRoles.Contains(role))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Role is invalid!");
        }
    }
}
