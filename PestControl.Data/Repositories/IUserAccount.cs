using PestControl.Domain.Requests;
using static PestControl.Domain.ResponseModels.ServiceResponse;

namespace PestControl.Data.Repositories;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAccount(UserRequest request);
    Task<LoginResponse> LoginAccount(LoginRequest request);
}
