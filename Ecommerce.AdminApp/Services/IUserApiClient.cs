using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.System.Users;

namespace Ecommerce.AdminApp.Services
{
	public interface IUserApiClient
	{
		Task<ApiResult<string>> Authenticate(LoginRequest request);

		Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request);

		Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);

		Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

		Task<ApiResult<UserVm>> GetById(Guid id);
	}
}
