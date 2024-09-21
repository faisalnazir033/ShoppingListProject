using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Data;

namespace ShoppingList.Infrastructure.Repositories;
public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}
public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext context = context;
    public async Task<User> GetUserByEmailAsync(string email) => await context.Users.SingleOrDefaultAsync(u => u.Email == email);
    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}