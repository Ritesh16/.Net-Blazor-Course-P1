using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductDto> Create(ProductDto ProductDto)
        {
            var product = _mapper.Map<Product>(ProductDto);

            var addedProduct = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(addedProduct.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<ProductDto> Get(int id)
        {
            var product = await _context.Products.Include(x => x.Category)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return _mapper.Map<Product, ProductDto>(product);
            }

            return new ProductDto();
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(await _context.Products
                    .Include(x => x.Category).ToListAsync());
        }

        public async Task<ProductDto> Update(ProductDto productDto)
        {
            var product = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productDto.Id);
            if (product != null)
            {
                product.Name = productDto.Name;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return _mapper.Map<Product, ProductDto>(product);
            }

            return productDto;
        }
    }
}
