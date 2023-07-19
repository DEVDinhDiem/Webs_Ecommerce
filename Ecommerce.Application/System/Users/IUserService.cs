﻿using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.System.Users
{
    public interface IUserService
    {
		Task<ApiResult<string>> Authencate(LoginRequest request);

		Task<ApiResult<bool>> Register(RegisterRequest request);

		Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

		Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

		Task<ApiResult<UserVm>> GetById(Guid id);

		Task<ApiResult<bool>> Delete(Guid id);

		Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
	}
}
