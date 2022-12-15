using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDto> Create(ProductPriceDto productPriceDto);
        public Task<ProductPriceDto> Update(ProductPriceDto productPriceDto);
        public Task<int> Delete(int id);
        public Task<ProductPriceDto> Get(int id);
        public Task<IEnumerable<ProductPriceDto>> GetAll(int? id = null);
    }
}
