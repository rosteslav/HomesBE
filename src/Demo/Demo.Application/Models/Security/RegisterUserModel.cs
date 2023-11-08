using Demo.Application.Attributes.Security;
using Demo.Application.Models.Security.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Models.Security
{
	public class RegisterUserModel : RegisterModel
    {
        [Required(ErrorMessage = "Role is required")]
        [ValidAppRole]
        public required AppRole Role { get; set; }
    }
}