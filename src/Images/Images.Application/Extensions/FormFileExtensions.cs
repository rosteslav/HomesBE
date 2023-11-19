using Microsoft.AspNetCore.Http;

namespace BuildingMarket.Images.Application.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<byte[]> ToMemoryStream(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
