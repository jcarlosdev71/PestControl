using PestControl.Application.Interfaces;
using PestControl.Data.Repositories;
using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;

namespace PestControl.Application.ApiFlows.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserResponse> CreateUser(UserRequest request) 
    {
        return await _userRepository.CreateUser(request);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
    }

    public Task<UserResponse> GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }

    public async Task<IEnumerable<UserResponse>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }

    public async Task<UserResponse> UpdateUser(UpdateUserRequest request)
    {
        return await _userRepository.UpdateUser(request);
    }
}
