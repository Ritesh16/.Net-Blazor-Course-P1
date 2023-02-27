using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data;
using Tangy_Data.Entities;
using Tangy_Data.ViewModel;
using Tangy_Models.Dtos;
using Tangy_Common;

namespace Tangy_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderDto> Create(OrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<OrderDto, Order>(orderDto);
                _context.OrderHeaders.Add(order.OrderHeader);
                await _context.SaveChangesAsync();

                foreach (var details in order.OrderDetails)
                {
                    details.OrderHeaderId = order.OrderHeader.Id;
                }

                _context.OrderDetails.AddRange(order.OrderDetails);
                await _context.SaveChangesAsync();
                var output = new OrderDto()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDto>(order.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDto>>(order.OrderDetails).ToList()
                };

                output.OrderHeader.Email = orderDto.OrderHeader.Email;
                return output;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int id)
        {
            var orderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (orderHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _context.OrderDetails.Where(u => u.OrderHeaderId == id);

                _context.OrderDetails.RemoveRange(objDetail);
                _context.OrderHeaders.Remove(orderHeader);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDto> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _context.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _context.OrderDetails.Where(u => u.OrderHeaderId == id),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDto>(order);
            }
            return new OrderDto();
        }

        public async Task<IEnumerable<OrderDto>> GetAll(string? userId = null, string? status = null)
        {

            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _context.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _context.OrderDetails;

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id),
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(OrderFromDb);

        }

        public async Task<OrderHeaderDto> MarkPaymentSuccessful(int id)
        {
            var data = await _context.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDto();
            }
            if (data.Status == Constants.Status_Pending)
            {
                data.Status = Constants.Status_Confirmed;
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDto>(data);
            }
            return new OrderHeaderDto();
        }


        public async Task<OrderHeaderDto> UpdateHeader(OrderHeaderDto orderHeaderDTO)
        {
            if (orderHeaderDTO != null)
            {
                var orderHeaderFromDb = _context.OrderHeaders.FirstOrDefault(u => u.Id == orderHeaderDTO.Id);
                orderHeaderFromDb.Name = orderHeaderDTO.Name;
                orderHeaderFromDb.PhoneNumber = orderHeaderDTO.PhoneNumber;
                //orderHeaderFromDb.Carrier = orderHeaderDTO.Carrier;
                //orderHeaderFromDb.Tracking = orderHeaderDTO.Tracking;
                orderHeaderFromDb.StreetAddress = orderHeaderDTO.StreetAddress;
                orderHeaderFromDb.City = orderHeaderDTO.City;
                orderHeaderFromDb.State = orderHeaderDTO.State;
                orderHeaderFromDb.PostalCode = orderHeaderDTO.PostalCode;
                orderHeaderFromDb.Status = orderHeaderDTO.Status;

                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDto>(orderHeaderFromDb);
            }
            return new OrderHeaderDto();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _context.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }

            data.Status = status;
            
            if (status == Constants.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
