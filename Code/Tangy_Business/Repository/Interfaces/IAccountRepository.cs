﻿using Tangy_Models.Dtos;

namespace Tangy_Business.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<OutputDto> Register(RegisterDto model);
        Task<LoginResultDto> Login(LoginDto model);
        Task<IList<string>> GetUserRoles(string email);
    }
}
