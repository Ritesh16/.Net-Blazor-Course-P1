using Tangy_Models.Dtos;

namespace TangyWeb_Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResultDto> Login(LoginDto loginDto);
        Task<string> GetUserName();
    }
}
