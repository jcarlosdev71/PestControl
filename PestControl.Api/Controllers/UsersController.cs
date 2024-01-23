using Microsoft.AspNetCore.Mvc;
using PestControl.Application.Interfaces;
using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;

namespace PestControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] UserRequest request)
    {
        UserResponse user = new();

        try
        {
            user = await _userService.CreateUser(request);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        var users = await _userService.GetUsers();

        return Ok(users);
    }

    [HttpGet("GetUser")]
    public async Task<ActionResult<UserResponse>> GetUserById([FromQuery] int id)
    {
        var user = await _userService.GetUser(id);

        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var user = await _userService.UpdateUser(request);

        return Ok(user);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser([FromQuery] int id)
    {
        await _userService.DeleteUser(id);

        return Ok();
    }
}
