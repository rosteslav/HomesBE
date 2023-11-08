using Demo.Application.Attributes.Security;
using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Models.Security
{
    public class RegisterUserModel : RegisterModel
    {
        [Required(ErrorMessage = "Role is required")]
        [ValidRole]
        public required string Role { get; set; }
    }
}
