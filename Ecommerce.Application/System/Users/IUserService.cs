﻿using Ecommerce.ViewModels.System.Users;

namespace Ecommerce.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);
    }
}
