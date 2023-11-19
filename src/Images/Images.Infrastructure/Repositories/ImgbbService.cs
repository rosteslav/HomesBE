using BuildingMarket.Images.Application.Contracts;
using BuildingMarket.Images.Application.Extensions;
using BuildingMarket.Images.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BuildingMarket.Images.Infrastructure.Repositories
{
    public class ImgbbService(
        ILogger<ImgbbService> logger,
        IConfiguration configuration) : IImgbbService
    {
        private readonly ILogger<ImgbbService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task DeleteImage(string deleteUrl)
        {
            try
            {
                _logger.LogInformation("Attempting to remove image from imgbb with deleteUrl: {deleteUrl}", deleteUrl);

                using HttpClient client = new();

                var requestData = new MultipartFormDataContent
                {
                    { new StringContent(_configuration.GetSection("Imgbb").Value), "key" }
                };

                await client
                    .GetAsync(deleteUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message} Image delete with deleteUrl: {deleteUrl} was not successful!", ex.Message, deleteUrl);
            }
        }

        public async Task<ImageData> UploadImage(ImageDTO image)
        {
            try
            {
                _logger.LogInformation("Attempting to upload image to imgbb with filename: {FileName}", image.FileName);

                var memoryStream = await FormFileExtensions
                    .ToMemoryStream(image.FormFile);

                string base64Image = Convert
                    .ToBase64String(memoryStream);

                using HttpClient client = new();

                var requestData = new MultipartFormDataContent
                {
                    { new StringContent(_configuration.GetSection("Imgbb").Value), "key" },
                    { new StringContent(base64Image), "image" },
                    { new StringContent(image.FileName), "name" }
                };

                var response = await client
                    .PostAsync("https://api.imgbb.com/1/upload", requestData);

                var jsonContent = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<ImageResponse>(jsonContent);

                return data.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message} Image upload with filename: {FileName} was not successful!", ex.Message, image.FileName);
            }

            return null;
        }
    }
}
