using Tangy_Models.Dtos;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAll(string? userId);
        public Task<OrderDto> Get(int orderId);

    }
}
