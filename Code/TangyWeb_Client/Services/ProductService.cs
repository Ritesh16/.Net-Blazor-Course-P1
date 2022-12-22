using Newtonsoft.Json;
using Tangy_Models.Dtos;
using TangyWeb_Client.Services.Interfaces;

namespace TangyWeb_Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<ProductDto> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/Product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(content);
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
                return products;
            }

            return new List<ProductDto>();  
        }
    }
}
