using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<OutputDto> Register(RegisterDto model);
    }
}
