using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BuildingMarket.Admins.Application.Features.Admins.Commands.AddNeighbourhoodsRating
{
    public class AddNeighbourhoodsRatingCommand : IRequest, IValidatableObject
    {
        [JsonPropertyName("for_living")]
        public required IEnumerable<IEnumerable<string>> ForLiving { get; set; }

        [JsonPropertyName("for_investment")]
        public required IEnumerable<IEnumerable<string>> ForInvestment { get; set; }

        public required IEnumerable<IEnumerable<string>> Budget { get; set; }

        public required IEnumerable<IEnumerable<string>> Luxury { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ForLiving.Count() != 2)
                yield return new ValidationResult("Invalid neighbourhoods for living.");

            if (ForInvestment.Count() != 2)
                yield return new ValidationResult("Invalid neighbourhoods for investment.");

            if (Budget.Count() != 2)
                yield return new ValidationResult("Invalid budget neighbourhoods.");

            if (Luxury.Count() != 2)
                yield return new ValidationResult("Invalid luxury neighbourhoods.");
        }
    }
}
