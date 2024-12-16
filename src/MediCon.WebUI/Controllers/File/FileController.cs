using System.Configuration;
using System.Web;

using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Settings;

using Microsoft.AspNetCore.Mvc;

namespace MediCon.WebUI.Controllers.File
{
    public class FileController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FileController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetFile(string filePath, string fileName)
        {
            var apiUrlSettings = ApiUrlSettings.Get(_configuration);
            //var apiUrl = "https://localhost:7261" + $"/api/file/{HttpUtility.UrlEncode(filePath)}?download=true&name=" + fileName;
            var apiUrl = apiUrlSettings.FileAPIBaseUrl + $"/api/file/{HttpUtility.UrlEncode(filePath)}?download=true&name=" + fileName;
            var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
            //var result = await _httpClient.GetAsync(apiUrl);
            var result = await client.GetAsync(apiUrl);

            if (!result.IsSuccessStatusCode)
            {
                return Json(new { Success = false });
            }

            var imageContent = await result.Content.ReadAsByteArrayAsync();
            if (result != null)
            {
                var contentType = result.Content?.Headers?.ContentType?.ToString();
                return File(imageContent, contentType ?? string.Empty, fileName);

            }
            return Json(new { Success = false });

        }
    }
}
