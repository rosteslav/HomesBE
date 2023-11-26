using BuildingMarket.Images.Application.Models;
using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IImgbbService
    {
        Task<ImageData> UploadImage(IFormFile image, string fileName);
    }
}
