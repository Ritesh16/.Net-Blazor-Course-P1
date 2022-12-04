using AutoMapper;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace Tangy_Business.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
