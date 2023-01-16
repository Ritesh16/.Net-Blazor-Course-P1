using Tangy_Models.Dtos;
using TangyWeb_Models;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<OutputDto> Register(RegisterDto registerModel);
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}
