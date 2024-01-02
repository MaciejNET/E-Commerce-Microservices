using ECommerce.Services.Users.Core.Entities;
using ECommerce.Services.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Users.Core.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
    }

    public Task<User> GetAsync(Guid id)
    {
        return _context.Users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<User> GetAsync(string email)
    {
        return _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}