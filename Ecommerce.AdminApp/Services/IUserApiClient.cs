using Ecommerce.ViewModels.Catalog.Common;
using Ecommerce.ViewModels.System.Users;

namespace Ecommerce.AdminApp.Services
{
	public interface IUserApiClient
	{
		Task<string> Authenticate(LoginRequest request);
		Task<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request);
        Task<bool> RegisterUser(RegisterRequest request);
    }
}
