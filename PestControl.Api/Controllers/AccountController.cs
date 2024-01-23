using Microsoft.AspNetCore.Mvc;
using PestControl.Data.Repositories;
using PestControl.Domain.Requests;

namespace PestControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
        {
            var response = await userAccount.CreateAccount(request);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await userAccount.LoginAccount(request);

            return Ok(response);
        }
    }
}
