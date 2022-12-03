using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> Create(CategoryDto categoryDto);
        Task<CategoryDto> Update(CategoryDto categoryDto);
        Task<int> Delete(int id);
        Task<CategoryDto> Get(int id);
        Task<IEnumerable<CategoryDto>> GetAll();

    }
}
