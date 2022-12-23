using Newtonsoft.Json;
using Tangy_Models.Dtos;
using TangyWeb_Client.Services.Interfaces;

namespace TangyWeb_Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string baseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this.baseServerUrl = _configuration.GetSection("BaseServerURL").Value;
        }
        public async Task<ProductDto> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/Product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(content);
                product.ImageUrl = baseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorDto = JsonConvert.DeserializeObject<ErrorModelDto>(content);
                throw new Exception(errorDto.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);
                foreach (var product in products)
                {
                    product.ImageUrl = baseServerUrl + product.ImageUrl;
                }

                return products;
            }

            return new List<ProductDto>();  
        }
    }
}
