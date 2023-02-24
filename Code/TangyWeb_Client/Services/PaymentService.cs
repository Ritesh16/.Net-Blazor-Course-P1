using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Tangy_Models.Dtos;
using TangyWeb_Client.Services.Interfaces;

namespace TangyWeb_Client.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SuccessModelDto> Checkout(StripePaymentDto paymentDto)
        {
            try
            {
                var content = JsonConvert.SerializeObject(paymentDto);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDto>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
