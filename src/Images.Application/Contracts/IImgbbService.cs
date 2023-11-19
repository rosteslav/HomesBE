using BuildingMarket.Images.Application.Models;

namespace BuildingMarket.Images.Application.Contracts
{
    public interface IImgbbService
    {
        Task<ImageData> UploadImage(ImageDTO image);
        Task DeleteImage(string deleteUrl);
    }
}
