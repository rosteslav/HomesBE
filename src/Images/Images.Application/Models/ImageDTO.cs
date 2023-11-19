using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Models
{
    public class ImageDTO
    {
        public IFormFile FormFile { get; set; }
        public string FileExtension { get; set; }
    }
}
