using Ecommerce.ViewModels.System.Users;

namespace Ecommerce.AdminApp.Services
{
	public interface IUserApiClient
	{
		Task<string> Authenticate(LoginRequest request);
	}
}
