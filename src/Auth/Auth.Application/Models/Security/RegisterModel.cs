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

        public string ImageUrl { get; set; }

        public string Purpose { get; set; }

        public string Region { get; set; }

        public string BuildingType { get; set; }

        [Range(0, 100_000_000_000)]
        public decimal PriceHigherEnd { get; set; }

        public string NumberOfRooms { get; set; }
    }
}
