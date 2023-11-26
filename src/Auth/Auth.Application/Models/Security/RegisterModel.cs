using System.ComponentModel.DataAnnotations;

namespace BuildingMarket.Auth.Application.Models.Security
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
