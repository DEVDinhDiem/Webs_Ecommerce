using Ecommerce.Application.System.Users;
using Ecommerce.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.BackendApi.Controllers
{
    [Route("~/api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or password is incorrect.");
            }
            return Ok(resultToken );
        }

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.Register(request);
			if (!result)
			{
				return BadRequest("Register is unsuccessful.");
			}
			return Ok();
		}

		//http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
		[HttpGet("paging")]
		public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
		{
			var Users = await _userService.GetUsersPaging(request);
			return Ok(Users);
		}

	}
}
