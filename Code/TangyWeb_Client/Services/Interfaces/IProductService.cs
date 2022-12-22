using Tangy_Models.Dtos;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAll();
        public Task<ProductDto> Get(int productId);
    }
}
