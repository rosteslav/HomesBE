using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Extensions;
using BuildingMarket.Images.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class ImgbbService(
        ILogger<ImgbbService> logger,
        IConfiguration configuration) : IImgbbService
    {
        private readonly ILogger<ImgbbService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ImageOutputModel> UploadImage(
            IFormFile image,
            string fileName)
        {
            try
            {
                _logger.LogInformation("Attempting to upload image to imgbb with filename: {FileName}", fileName);

                var memoryStream = await FormFileExtensions
                    .ToMemoryStream(image);

                string base64Image = Convert
                    .ToBase64String(memoryStream);

                using HttpClient client = new();

                var requestData = new MultipartFormDataContent
                {
                    { new StringContent(_configuration.GetSection("Imgbb").Value), "key" },
                    { new StringContent(base64Image), "image" },
                    { new StringContent(fileName), "name" }
                };

                var response = await client
                    .PostAsync("https://api.imgbb.com/1/upload", requestData);

                var jsonContent = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<ImageResponse>(jsonContent);

                return new() { DisplayUrl = data.ImageData.DisplayUrl };
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message} Image upload with filename: {FileName} was not successful!", ex.Message, image.Name);
            }

            return null;
        }
    }
}
