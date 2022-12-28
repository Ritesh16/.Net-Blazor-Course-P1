using Newtonsoft.Json;
using Tangy_Models.Dtos;
using TangyWeb_Client.Services.Interfaces;

namespace TangyWeb_Client.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
        public async Task<OrderDto> Get(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDto>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAll(string? userId = null)
        {
            var response = await _httpClient.GetAsync("/api/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(content);

                return orders;
            }

            return new List<OrderDto>();
        }
    }
}
