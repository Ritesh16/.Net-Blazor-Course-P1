using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace Tangy_Business.Mapper
{
    public class ProductPriceProfile : Profile
    {
        public ProductPriceProfile()
        {
            CreateMap<ProductPrice, ProductPriceDto>().ReverseMap();
        }
    }
}
