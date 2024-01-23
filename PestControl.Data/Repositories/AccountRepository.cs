using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PestControl.Domain.Entities;
using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static PestControl.Domain.ResponseModels.ServiceResponse;

namespace PestControl.Data.Repositories;

public class AccountRepository(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration config) : IUserAccount
{
    public async Task<ServiceResponse.GeneralResponse> CreateAccount(UserRequest request)
    {
        if (request is null) return new GeneralResponse(false, "Model is empty");
        var newUser = new ApplicationUser
        {
            Email = request.Email,
            Name = request.Name,
            PasswordHash = request.Password,
            UserName = request.Email
        };

        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user is not null) return new GeneralResponse(false, "User registered already");

        var createUser = await userManager.CreateAsync(newUser!, request.Password);
        if (!createUser.Succeeded) return new GeneralResponse(false, "Error occurred.. please try again");

        var checkAdmin = await roleManager.FindByNameAsync("Admin");
        if (checkAdmin is null)
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Account Created");
        }
        else
        {
            var checkUser = await roleManager.FindByNameAsync("User");
            if (checkUser is null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

            await userManager.AddToRoleAsync(newUser, "User");
            return new GeneralResponse(true, "Account Created");
        }
    }

    public async Task<LoginResponse> LoginAccount(LoginRequest request)
    {
        if (request is null)
            return new LoginResponse(false, null!, "Login container is empty");

        var getUser = await userManager.FindByEmailAsync(request.Email);
        if (getUser is null)
            return new LoginResponse(false, null!, "User not found");

        bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, request.Password);
        if (!checkUserPasswords)
            return new LoginResponse(false, null!, "Invalid email/password");

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
        string token = GenerateToken(userSession);
        return new LoginResponse(true, token, "Login completed");

    }

    private string GenerateToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim("UserId", user.Id),
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role)
        };

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            userClaims,
            DateTime.Now,
            DateTime.Now.AddDays(1),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
