using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Data.Entities;
using Tangy_Data.ViewModel;
using Tangy_Models.Dtos;

namespace Tangy_Business.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderHeaderDto, OrderHeader>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
        }
    }
}
