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
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductPriceRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductPriceDto> Create(ProductPriceDto productPriceDTO)
        {
            var productPrice = _mapper.Map<ProductPriceDto, ProductPrice>(productPriceDTO);

            var addedproductPrice = _context.ProductPrices.Add(productPrice);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDto>(addedproductPrice.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var productPrice = await _context.ProductPrices.FirstOrDefaultAsync(u => u.Id == id);
            if (productPrice != null)
            {
                _context.ProductPrices.Remove(productPrice);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDto> Get(int id)
        {
            var productPrice = await _context.ProductPrices.FirstOrDefaultAsync(u => u.Id == id);
            if (productPrice != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDto>(productPrice);
            }
            return new ProductPriceDto();
        }

        public async Task<IEnumerable<ProductPriceDto>> GetAll(int? id = null)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>
                    (_context.ProductPrices.Where(u => u.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>(_context.ProductPrices);
            }
        }

        public async Task<ProductPriceDto> Update(ProductPriceDto productPriceDTO)
        {
            var productPriceFromDb = await _context.ProductPrices.FirstOrDefaultAsync(u => u.Id == productPriceDTO.Id);
            if (productPriceFromDb != null)
            {
                productPriceFromDb.Price = productPriceDTO.Price;
                productPriceFromDb.Size = productPriceDTO.Size;
                productPriceFromDb.ProductId = productPriceDTO.ProductId;
                _context.ProductPrices.Update(productPriceFromDb);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDto>(productPriceFromDb);
            }
            return productPriceDTO;

        }
    }
}
