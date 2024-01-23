using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;

namespace PestControl.Data.Repositories;
public interface IUserRepository
{
    Task<UserResponse> CreateUser(UserRequest request);
    Task<IEnumerable<UserResponse>> GetUsers();
    Task<UserResponse> GetUser(int id);
    Task<UserResponse> UpdateUser(UpdateUserRequest request);
    Task DeleteUser(int id);
}
