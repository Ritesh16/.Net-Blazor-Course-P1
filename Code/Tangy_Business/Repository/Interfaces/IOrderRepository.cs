using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<OrderDto> Get(int id);
        public Task<IEnumerable<OrderDto>> GetAll(string? userId = null, string? status = null);
        public Task<OrderDto> Create(OrderDto orderDTO);
        public Task<int> Delete(int id);

        public Task<OrderHeaderDto> UpdateHeader(OrderHeaderDto orderHeaderDTO);

        public Task<OrderHeaderDto> MarkPaymentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);

    }
}
