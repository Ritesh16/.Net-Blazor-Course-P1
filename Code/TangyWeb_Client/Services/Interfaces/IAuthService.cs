using Tangy_Models.Dtos;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<OutputDto> Register(RegisterDto registerModel);
        Task<LoginResultDto> Login(LoginDto loginModel);
        Task Logout();
    }
}
