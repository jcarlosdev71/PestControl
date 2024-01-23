using Microsoft.EntityFrameworkCore;
using PestControl.Data.Context;
using PestControl.Domain.Entities;
using PestControl.Domain.Requests;
using PestControl.Domain.ResponseModels;

namespace PestControl.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PestControlDbContext _context;

    public UserRepository(PestControlDbContext context)
    {
        _context = context;
    }
    public async Task<UserResponse> CreateUser(UserRequest request)
    {
        var newUser = new ApplicationUser
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Dni = request.Dni
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Dni = newUser.Dni,
            Email = newUser.Email,
            LastName = newUser.LastName,
            Name = newUser.Name
        };
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserResponse> GetUser(int id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id.ToString());

        return new UserResponse
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Dni = user.Dni
        };
    }

    public async Task<IEnumerable<UserResponse>> GetUsers()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();

        List<UserResponse> userList = new();

        foreach (var user in users)
        {
            userList.Add(new UserResponse
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Dni = user.Dni
            });
        }

        return userList;
    }

    public async Task<UserResponse> UpdateUser(UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(request.Id);

        user.Email = request.Email;
        user.Dni = request.Dni;
        user.Name = request.Name;
        user.LastName = request.LastName;

        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Dni = user.Dni
        };
    }
}
