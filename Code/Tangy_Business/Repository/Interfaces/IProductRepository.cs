using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> Create(ProductDto categoryDto);
        Task<ProductDto> Update(ProductDto categoryDto);
        Task<int> Delete(int id);
        Task<ProductDto> Get(int id);
        Task<IEnumerable<ProductDto>> GetAll();
    }
}
