using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces;
public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}
