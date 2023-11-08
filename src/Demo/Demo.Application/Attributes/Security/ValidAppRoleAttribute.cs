using Demo.Application.Models.Security.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Attributes.Security
{
	public class ValidAppRoleAttribute : ValidationAttribute
	{
		private readonly IEnumerable<string> appRoles = Enum.GetNames(typeof(AppRole));

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is AppRole role && appRoles.Contains(role.ToString()))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult("Role is invalid!");
		}
	}
}