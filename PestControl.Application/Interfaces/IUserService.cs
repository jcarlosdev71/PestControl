using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;

namespace PestControl.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUser(UserRequest request);
    Task<IEnumerable<UserResponse>> GetUsers();
    Task<UserResponse> GetUser(int id);
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
    Task DeleteUser(int id);
}
