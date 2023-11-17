using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingMarket.Images.Domain.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public string ImageName { get; set; } = null!;

        [Required]
        public string ImageURL { get; set; } = null!;

        [Required]
        public string DeleteURL { get; set; } = null!;
    }
}
